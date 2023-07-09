using AutoMapper;
using Rent.API.DTOs;
using Rent.Domain.Entities;

namespace Rent.API.ConfigurationInjector
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region DTOToDomain
            CreateMap<CarDTO, Car>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<DocumentDTO, Document>();
            //CreateMap<Holder, Card>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region DomainToDTO
            CreateMap<Car, CarDTO>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Document, DocumentDTO>();
            //CreateMap<Card, Holder>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion
        }
    }
}
