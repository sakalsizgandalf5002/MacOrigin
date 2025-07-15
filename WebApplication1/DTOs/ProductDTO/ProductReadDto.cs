using WebApplication1.Models;

namespace WebApplication1.DTOs.ProductDTO;

public class ProductReadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public string CategoryName { get; set; } = default!;
}