using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Cart;

namespace FitVerse.Web.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //CreateMap<Product,CartItem_ViewModel>().ReverseMap();
            CreateMap<Product,CartItem_ViewModel>().AfterMap((src,dest) => {
                dest.Prod_Id = src.Id;
                dest.Prod_Name = src.Name;
            }).ReverseMap();
            CreateMap<CartItem, CartItem_ViewModel>().AfterMap((src, dest) => {
                dest.Cart_Id = src.Id;
                dest.Prod_Id= src.ProductId;
            }).ReverseMap();
            
            CreateMap<(Product product, CartItem cartItem), CartItem_ViewModel>()
           .ForMember(dest => dest.Prod_Id, opt => opt.MapFrom(src => src.product.Id))
           .ForMember(dest => dest.Prod_Name, opt => opt.MapFrom(src => src.product.Name))
           .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.cartItem.Quantity))
           .ForMember(dest => dest.Cart_Id, opt => opt.MapFrom(src => src.cartItem.Id))
           .ReverseMap();
           // Map other cart item properties
           ;
        }
    }
}
