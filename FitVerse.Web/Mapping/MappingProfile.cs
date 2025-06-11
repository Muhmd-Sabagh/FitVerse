using AutoMapper;
using E_commerce_Khotwa.Models;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, Product_ViewModel>().ReverseMap();
    }
}
