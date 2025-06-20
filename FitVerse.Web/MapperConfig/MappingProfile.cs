using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Product;

namespace FitVerse.Web.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductCardViewModel>().ReverseMap();
            CreateMap<Product, ProductDetailsViewModel>().AfterMap((src, dest) =>
            {
                dest.ParentCategoryName = src.Category.ParentCategory.Name;
                dest.CategoryName = src.Category.Name;
            });
            CreateMap<ProductFormAddData, Product>().ReverseMap();
            CreateMap<ProductFormEditData, Product>().ReverseMap();
        }
    }
}
