using AutoMapper;
using Rent.Domain.DTO.Request;
using Rent.Domain.DTO.Request.Create;
using Rent.Domain.DTO.Request.Update;
using Rent.Domain.DTO.Response;
using Rent.Domain.Entities;

namespace Rent.API.ConfigurationInjector
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region CreateDTO to Model
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<CreateCustomerDTO, Customer>();
            #endregion

            #region Model to CreateDTO
            CreateMap<Employee, CreateEmployeeDTO>();
            CreateMap<Customer, CreateCustomerDTO>();
            #endregion

            #region UpdateDTO to Model
            CreateMap<UpdateEmployeeDTO, Employee>();
            CreateMap<UpdateCustomerDTO, Customer>();
            #endregion

            #region Model to UpdateDTO
            CreateMap<Employee, UpdateEmployeeDTO>();
            CreateMap<Customer, UpdateCustomerDTO>();
            #endregion

            #region DTO to Model
            CreateMap<CarDTO, Car>();
            CreateMap<CustomerDTO, Customer>();
            CreateMap<EmployeeDTO, Employee>();
            CreateMap<OwnerDTO, Owner>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<LoginRequest, Login>();
            CreateMap<LoginDTO, Login>();
            //CreateMap<Holder, Card>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Model to DTO
            CreateMap<Car, CarDTO>();
            CreateMap<Customer, CustomerDTO>();
            CreateMap<Employee, EmployeeDTO>();
            CreateMap<Owner, OwnerDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Login, LoginRequest>();
            CreateMap<Login, LoginDTO>();
            //CreateMap<Card, Holder>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion
        }
    }
}
