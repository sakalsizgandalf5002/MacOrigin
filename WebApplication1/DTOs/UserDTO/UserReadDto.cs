namespace WebApplication1.DTOs.UserDTO;

public class UserReadDto
{
    public int Id { get; set; }
    public string Username { get; set; } = default!;
    public string Email { get; set; } = default!;
}