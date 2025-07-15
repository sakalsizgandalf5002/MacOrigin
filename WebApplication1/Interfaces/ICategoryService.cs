using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplication1.DTOs.CategoryDTO;

namespace WebApplication1.Services.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryReadDto>> GetAllAsync();
    Task<CategoryReadDto?>             GetByIdAsync(int id);
    Task<CategoryReadDto>              CreateAsync(CategoryCreateDto dto);
    Task<bool>                         UpdateAsync(int id, CategoryUpdateDto dto);
    Task<bool>                         DeleteAsync(int id);
}