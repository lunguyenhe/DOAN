using AutoMapper;

using System.Security;
  //.ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.FirstName + " " + src.Employee.LastName));
namespace DOAN.DTO
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
        //    CreateMap<Product, ProductDTO>()
        //.ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.CategoryName ))
        //.ForMember(dest => dest.BrandName, opt => opt.MapFrom(src => src.Brand.BrandName ));
       
		}
    }
}
