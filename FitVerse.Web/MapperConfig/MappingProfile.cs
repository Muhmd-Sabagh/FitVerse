using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Category;
using FitVerse.Web.ViewModels.Home;
using FitVerse.Web.ViewModels.Product;

namespace FitVerse.Web.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product,CartItem_ViewModel>().ReverseMap();
            CreateMap<Product, CartItem_ViewModel>().AfterMap((src,dest) => {
                dest.ProductId = src.Id;
                dest.Prod_Name = src.Name;
            }).ReverseMap();
            CreateMap<CartItem, CartItem_ViewModel>().AfterMap((src, dest) => {
                dest.Cart_Id = src.Id;
            }).ReverseMap();
            CreateMap<Product,ProductViewModel>().ReverseMap();
            CreateMap<Category,CategoryViewModel>().ReverseMap();
            CreateMap<Banner, BannarHomeViewModel>().ReverseMap();
        }
    }
}
