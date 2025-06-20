using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.ViewModels.Cart;
using FitVerse.Web.ViewModels.Checkout;
using FitVerse.Web.ViewModels.Product;


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
            CreateMap<CartItem, OrderItem>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Id, opt => opt.Ignore())            ;
            CreateMap<Order_ViewModel, Order>()
                .ReverseMap();
        
            ////CreateMap<OrderItemRepository,OrderItem>().ReverseMap();
            //CreateMap<OrderViewModel, Order>().ReverseMap()
            //    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id)) // Ensure Id is mapped if it exists on Order entity
            //.ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems)); ;

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
