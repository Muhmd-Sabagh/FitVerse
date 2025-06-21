using AutoMapper;
using FitVerse.Web.Models;
using FitVerse.Web.ViewModels;
using FitVerse.Web.ViewModels.Account;
using FitVerse.Web.ViewModels.Cart;
using FitVerse.Web.ViewModels.Category;
using FitVerse.Web.ViewModels.Checkout;
using FitVerse.Web.ViewModels.Home;
using FitVerse.Web.ViewModels.Product;

namespace FitVerse.Web.MapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Mappings for Account ViewModels
            CreateMap<RegisterViewModel, ApplicationUser>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
            CreateMap<LoginViewModel, ApplicationUser>().ReverseMap();

            // Mappings for Cart ViewModels
            CreateMap<Product, CartItem_ViewModel>()
                .ForMember(dest => dest.Prod_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Prod_Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.DiscountPercentage))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.ImageUrl))
                .ForMember(dest => dest.EffectivePrice, opt => opt.MapFrom(src => src.EffectivePrice))
                .ReverseMap();

            // Mapping from CartItem to CartItem_ViewModel
            CreateMap<CartItem, CartItem_ViewModel>()
                .ForMember(dest => dest.Cart_Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Prod_Id, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.Prod_Name, opt => opt.Ignore())
                .ForMember(dest => dest.Price, opt => opt.Ignore())
                .ForMember(dest => dest.DiscountPercentage, opt => opt.Ignore())
                .ForMember(dest => dest.ImageUrl, opt => opt.Ignore())
                .ForMember(dest => dest.EffectivePrice, opt => opt.Ignore())
                .ReverseMap();

            // Mapping from a tuple of (Product, CartItem) to CartItem_ViewModel
            CreateMap<(Product Product, CartItem CartItem), CartItem_ViewModel>()
                .ForMember(dest => dest.Cart_Id, opt => opt.MapFrom(src => src.CartItem.Id))
                .ForMember(dest => dest.Prod_Id, opt => opt.MapFrom(src => src.Product.Id))
                .ForMember(dest => dest.Prod_Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.CartItem.Quantity))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.DiscountPercentage, opt => opt.MapFrom(src => src.Product.DiscountPercentage))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
                .ForMember(dest => dest.EffectivePrice, opt => opt.MapFrom(src => src.Product.EffectivePrice));

            // Mappings for Checkout and Order
            CreateMap<CartItem, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Order, Order_ViewModel>()
                 .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                 .ForMember(dest => dest.OrderDate, opt => opt.MapFrom(src => src.OrderDate))
                 .ForMember(dest => dest.TotalAmount, opt => opt.MapFrom(src => src.TotalAmount))
                 .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
                 .ForMember(dest => dest.ShippingAddress, opt => opt.MapFrom(src => src.ShippingAddress))
                 .ForMember(dest => dest.CustomerName, opt => opt.MapFrom(src => src.CustomerName))
                 .ForMember(dest => dest.CustomerEmail, opt => opt.MapFrom(src => src.CustomerEmail))
                 .ForMember(dest => dest.CustomerPhone, opt => opt.MapFrom(src => src.CustomerPhone))
                 .ForMember(dest => dest.OrderItems, opt => opt.MapFrom(src => src.OrderItems))
                 .ReverseMap();

            CreateMap<OrderItem, ViewModels.Order.OrderItemViewModel>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.UnitPrice, opt => opt.MapFrom(src => src.UnitPrice))
                .ForMember(dest => dest.Prod_Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => src.Product.ImageUrl))
                .ReverseMap();

            // Mappings for Product ViewModels
            CreateMap<Product, ProductCardViewModel>().ReverseMap();

            CreateMap<Product, ProductDetailsViewModel>()
                .ForMember(dest => dest.ParentCategoryName, opt => opt.MapFrom(src => src.Category.ParentCategory.Name))
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ReverseMap();

            CreateMap<ProductFormAddData, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<ProductFormEditData, Product>()
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ReverseMap();

            CreateMap<Product, ProductViewModel>()
                .ReverseMap();

            // Mappings for Category ViewModels
            CreateMap<Category, CategoryViewModel>().ReverseMap();

            // Mappings for Home ViewModels
            CreateMap<Banner, BannarHomeViewModel>().ReverseMap();
            CreateMap<Category, HomeViewModel>().ReverseMap();

            // Mapping for the main HomeViewModel.
            CreateMap<HomeViewModel, HomeViewModel>();

            // Mapping for DetailsViewModel
            CreateMap<Product, DetailsViewModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.ParentCategory, opt => opt.MapFrom(src => src.Category.ParentCategory.Name))
                .ForMember(dest => dest.EffectivePrice, opt => opt.MapFrom(src => (int)src.EffectivePrice))
                .ForMember(dest => dest.IsOnSale, opt => opt.MapFrom(src => src.IsOnSale))
                .ReverseMap();

            // Mapping for RoleViewModel
            CreateMap<RoleViewModel, Microsoft.AspNetCore.Identity.IdentityRole>().ReverseMap();
        }
    }
}
