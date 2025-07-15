using AutoMapper;
using WebApplication1.Data;
using WebApplication1.DTOs.CategoryDTO;
using WebApplication1.Models;
using WebApplication1.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Services;

public class CategoryServices : ICategoryService
{
    private readonly AppDbContext _context;
    private readonly IMapper      _mapper;

    public CategoryServices(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper  = mapper;
    }

    public async Task<IEnumerable<CategoryReadDto>> GetAllAsync()
    {
        var categories = await _context.Categories.AsNoTracking().ToListAsync();
        return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
    }

    public async Task<CategoryReadDto?> GetByIdAsync(int id)
    {
        var category = await _context.Categories.AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == id);
        return category == null ? null : _mapper.Map<CategoryReadDto>(category);
    }

    public async Task<CategoryReadDto> CreateAsync(CategoryCreateDto dto)
    {
        var category = _mapper.Map<Category>(dto);
        _context.Categories.Add(category);
        await _context.SaveChangesAsync();
        return _mapper.Map<CategoryReadDto>(category);
    }

    public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _mapper.Map(dto, category);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) return false;

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}