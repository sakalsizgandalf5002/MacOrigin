namespace WebApplication1.Models;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
    
    public string PasswordHash { get; set; } = default!; // Argon2 hash string’i

    public string Role { get; set; } = default!;

    public string RefreshToken { get; set; } = default!;
    public DateTime RefreshTokenExpiryTime { get; set; }
}
