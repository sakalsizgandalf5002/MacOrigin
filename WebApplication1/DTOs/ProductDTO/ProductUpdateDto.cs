namespace WebApplication1.DTOs.ProductDTO;

public class ProductUpdateDto
{
    public string? Name { get; set; }
    public decimal? Price { get; set; }
    public int? Stock { get; set; }
    public int? CategoryId { get; set; }
}