using AutoMapper;
using WebApplication1.Data;
using WebApplication1.DTOs.UserDTO;
using WebApplication1.Models;
using WebApplication1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services;

public class UserService : IUserService
{
    private readonly AppDbContext    _context;
    private readonly IMapper         _mapper;
    private readonly IPasswordHasher _hasher;
    private readonly ITokenService   _token;

    public UserService(AppDbContext context,
                        IMapper mapper,
                        IPasswordHasher hasher,
                        ITokenService token)
    {
        _context = context;
        _mapper  = mapper;
        _hasher  = hasher;
        _token   = token;
    }

    public async Task<IEnumerable<UserReadDto>> GetAllAsync()
    {
        var users = await _context.Users.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<UserReadDto>>(users);
    }

    public async Task<UserReadDto?> GetByIdAsync(int id)
    {
        var user = await _context.Users.AsNoTracking()
                                       .FirstOrDefaultAsync(u => u.Id == id);
        return user == null ? null : _mapper.Map<UserReadDto>(user);
    }

    public async Task RegisterAsync(UserRegisterDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new InvalidOperationException("Email already in use.");

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = _hasher.HashPassword(dto.Password);
        user.Role         = "Customer";

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task<AuthDto> LoginAsync(UserLoginDto dto)
    {
        var user = await _context.Users
            .SingleOrDefaultAsync(u => u.Email == dto.Email)
            ?? throw new InvalidOperationException("User not found.");

        if (!_hasher.VerifyPassword(dto.Password, user.PasswordHash))
            throw new InvalidOperationException("Invalid credentials.");

        var accessToken = _token.CreateToken(user);
        var refreshToken = _token.GenerateRefreshToken();
        user.RefreshTokens.Add(refreshToken);
        await _context.SaveChangesAsync();
        return new AuthDto(accessToken, refreshToken.Token.ToString());
    }


    public async Task CreateAsync(UserCreateDto dto)
    {
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new InvalidOperationException("Email already in use.");

        var user = _mapper.Map<User>(dto);
        user.PasswordHash = _hasher.HashPassword(dto.Password);
        user.Role         = "Customer";

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(int id, UserUpdateDto dto)
    {
        var user = await _context.Users.FindAsync(id)
                   ?? throw new InvalidOperationException("User not found.");

        _mapper.Map(dto, user);

        if (!string.IsNullOrWhiteSpace(dto.Password))
            user.PasswordHash = _hasher.HashPassword(dto.Password);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
    public async Task<AuthDto> RefreshAsync(Guid token)
    {
        var rt = await _context.RefreshTokens
            .Include(r => r.User)
            .SingleOrDefaultAsync(r =>
                r.Token == token && !r.Revoked && r.Expires > DateTime.UtcNow)
            ?? throw new InvalidOperationException("Invalid refresh token.");

        rt.Revoked = true;

        var access = _token.CreateToken(rt.User);
        var newRt = _token.GenerateRefreshToken();
        rt.User.RefreshTokens.Add(newRt);

        await _context.SaveChangesAsync();

        return new AuthDto(access, newRt.Token.ToString());
    }

}
