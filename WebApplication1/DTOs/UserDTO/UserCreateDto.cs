namespace WebApplication1.DTOs.UserDTO;

public class UserCreateDto
{

        public string Username { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
}