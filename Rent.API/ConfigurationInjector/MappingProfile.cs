using AutoMapper;
using Rent.API.DTOs;
using Rent.Domain.Entities;

namespace Rent.API.ConfigurationInjector
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region DTO to Model
            CreateMap<CarDTO, Car>();
            CreateMap<LoginDTO, TokenResponse>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<LoginRequest, Login>();
            //CreateMap<Holder, Card>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Model to DTO
            CreateMap<Car, CarDTO>();
            CreateMap<TokenResponse, LoginDTO>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Login, LoginRequest>();
            //CreateMap<Card, Holder>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion
        }
    }
}
