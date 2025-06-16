using AutoMapper;
using FitVerse.Web.Models;

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
        }
    }
}
