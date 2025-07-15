namespace WebApplication1.Models;

public class Category
{
   public int Id { get; set; }
   public string Name { get; set; } = default!;
   public ICollection<Product> Products = new List<Product>();
}