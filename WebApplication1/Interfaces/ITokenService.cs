using WebApplication1.Models;

namespace WebApplication1.Interfaces;

public interface ITokenService
{
    string CreateToken(User user);
    RefreshToken GenerateRefreshToken();

}