using AutoMapper;
using Rent.Domain.DTOs;
using Rent.Domain.DTOs.Response;
using Rent.Domain.Entities;

namespace Rent.API.ConfigurationInjector
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region DTO to Model
            CreateMap<CarDTO, Car>();
            CreateMap<TokenResponseDTO, TokenResponse>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<LoginRequest, Login>();
            //CreateMap<Holder, Card>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Model to DTO
            CreateMap<Car, CarDTO>();
            CreateMap<TokenResponse, TokenResponseDTO>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<Login, LoginRequest>();
            //CreateMap<Card, Holder>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion
        }
    }
}
