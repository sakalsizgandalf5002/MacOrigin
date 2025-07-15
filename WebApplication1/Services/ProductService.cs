using AutoMapper;
using WebApplication1.Data;
using WebApplication1.DTOs.ProductDTO;
using WebApplication1.Models;
using WebApplication1.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services;

public class ProductServices : IProductService
{
    private readonly AppDbContext _context;
    private readonly IMapper      _mapper;

    public ProductServices(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper  = mapper;
    }

    public async Task<IEnumerable<ProductReadDto>> GetAllAsync()
    {
        var products = await _context.Products.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<ProductReadDto>>(products);
    }

    public async Task<ProductReadDto?> GetByIdAsync(int id)
    {
        var product = await _context.Products.AsNoTracking()
            .FirstOrDefaultAsync(p => p.Id == id);
        return product == null ? null : _mapper.Map<ProductReadDto>(product);
    }

    public async Task<ProductReadDto> CreateAsync(ProductCreateDto dto)
    {
        var product = _mapper.Map<Product>(dto);
        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return _mapper.Map<ProductReadDto>(product);
    }

    public async Task<bool> UpdateAsync(int id, ProductUpdateDto dto)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _mapper.Map(dto, product);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var product = await _context.Products.FindAsync(id);
        if (product == null) return false;

        _context.Products.Remove(product);
        await _context.SaveChangesAsync();
        return true;
    }
}