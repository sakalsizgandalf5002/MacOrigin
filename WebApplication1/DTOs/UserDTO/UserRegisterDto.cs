using System.ComponentModel.DataAnnotations;

namespace WebApplication1.DTOs.UserDTO;

public class UserRegisterDto
{
    public string Username { get; set; } = default!;
    public string Password { get; set; } = default!;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = default!;
}