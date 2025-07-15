using WebApplication1.DTOs.ProductDTO;
namespace WebApplication1.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductReadDto>> GetAllAsync();
    Task<ProductReadDto?>             GetByIdAsync(int id);
    Task<ProductReadDto>              CreateAsync(ProductCreateDto dto);
    Task<bool>                        UpdateAsync(int id, ProductUpdateDto dto);
    Task<bool>                        DeleteAsync(int id);
}