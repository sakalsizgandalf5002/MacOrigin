using AutoMapper;
using WebApplication1.DTOs.CategoryDTO;
using WebApplication1.DTOs.ProductDTO;
using WebApplication1.DTOs.UserDTO;
using WebApplication1.Models;

namespace WebApplication1.Mappings;

public class MappingProfile : Profile
{
  public MappingProfile()
  {
    CreateMap<User, UserReadDto>();
    CreateMap<UserCreateDto, User>();
    CreateMap<UserUpdateDto, User>();
    CreateMap<UserRegisterDto, User>();
    
    CreateMap<Product, ProductReadDto>();
    CreateMap<ProductCreateDto, Product>();
    CreateMap<ProductUpdateDto, Product>();
    
    CreateMap<Category, CategoryReadDto>();
    CreateMap<CategoryCreateDto, Category>();
    CreateMap<CategoryUpdateDto, Category>();
  }
}