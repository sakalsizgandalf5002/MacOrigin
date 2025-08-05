
using System.Text;
using WebApplication1.Interfaces;

namespace WebApplication1.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContext;
        private readonly AppDbContext _context;

        public TokenService(
            IOptions<JwtSettings> jwtOptions,
            IHttpContextAccessor httpContext,
            AppDbContext context)
        {
            _jwtSettings = jwtOptions.Value;
            _httpContext = httpContext;
            _context = context;
        }

        public string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiresInMinutes),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private string GetIpAddress() =>
            _httpContext.HttpContext?.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

        public RefreshToken GenerateRefreshToken()
        {
            return new RefreshToken
            {
                Token = Guid.NewGuid(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = GetIpAddress(),
                Revoked = false
            };
        }

        public async Task<AuthDto> RefreshAsync(Guid token)
        {
            var ip = GetIpAddress();
            var rt = await _context.RefreshTokens
                .Include(r => r.User)
                .SingleOrDefaultAsync(r =>
                    r.Token == token &&
                    !r.Revoked &&
                    r.Expires > DateTime.UtcNow)
                ?? throw new InvalidOperationException("Invalid refresh token.");

            rt.Revoked = true;
            rt.RevokedByIp = ip;

            var newRt = GenerateRefreshToken();
            rt.User.RefreshTokens.Add(newRt);

            var access = CreateToken(rt.User);

            await _context.SaveChangesAsync();
            return new AuthDto(access, newRt.Token.ToString());
        }
    }
}
