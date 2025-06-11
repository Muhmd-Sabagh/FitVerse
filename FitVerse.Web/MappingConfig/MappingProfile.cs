using AutoMapper;
using FitVerse.Web.Models;

namespace FitVerse.Web.MappingConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
           CreateMap<Product, Product_ViewModel>().ReverseMap();
            
        }
    }
}
