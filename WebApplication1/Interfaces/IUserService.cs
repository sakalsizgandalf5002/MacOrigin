using WebApplication1.DTOs.UserDTO;
public interface IUserService
{
   
    Task RegisterAsync(UserRegisterDto dto);
    Task<AuthDto> LoginAsync(UserLoginDto dto);
    



    Task CreateAsync(UserCreateDto dto);   

    
    Task<UserReadDto>        GetByIdAsync(int id);
    Task<IEnumerable<UserReadDto>> GetAllAsync();

    
    Task UpdateAsync(int id, UserUpdateDto dto);
    Task DeleteAsync(int id);
}