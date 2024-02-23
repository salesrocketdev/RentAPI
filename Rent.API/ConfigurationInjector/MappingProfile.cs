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
            CreateMap<CreateCarDTO, Car>();
            CreateMap<CreateEmployeeDTO, Employee>();
            CreateMap<CreateCustomerDTO, Customer>();
            CreateMap<CreateOwnerDTO, Owner>();
            CreateMap<CreateRentalDTO, Rental>();
            CreateMap<CreateDocumentDTO, Document>();
            #endregion

            #region Model to CreateDTO
            CreateMap<Car, CreateCarDTO>();
            CreateMap<Employee, CreateEmployeeDTO>();
            CreateMap<Customer, CreateCustomerDTO>();
            CreateMap<Owner, CreateOwnerDTO>();
            CreateMap<Rental, CreateRentalDTO>();
            CreateMap<Document, CreateDocumentDTO>();
            #endregion

            #region UpdateDTO to Model
            CreateMap<UpdateCarDTO, Car>();
            CreateMap<UpdateEmployeeDTO, Employee>();
            CreateMap<UpdateCustomerDTO, Customer>();
            CreateMap<UpdateOwnerDTO, Owner>();
            CreateMap<UpdateRentalDTO, Rental>();
            #endregion

            #region Model to UpdateDTO
            CreateMap<Car, UpdateCarDTO>();
            CreateMap<Employee, UpdateEmployeeDTO>();
            CreateMap<Customer, UpdateCustomerDTO>();
            CreateMap<Owner, UpdateOwnerDTO>();
            CreateMap<Rental, UpdateRentalDTO>();
            #endregion

            #region DTO to Model
            CreateMap<ResponseCarDTO, Car>();
            CreateMap<CarImageDTO, CarImage>();
            CreateMap<ResponseCustomerDTO, Customer>();
            // CreateMap<ResponseCustomerDTO, Customer>()
            //     .ForMember(dest => dest.Document.CustomerId, opt => opt.MapFrom(src => src.Id));
            CreateMap<ResponseEmployeeDTO, Employee>();
            CreateMap<ResponseOwnerDTO, Owner>();
            CreateMap<ResponseRentalDTO, Rental>();
            CreateMap<DocumentDTO, Document>();
            CreateMap<LoginRequest, Login>();
            CreateMap<LoginDTO, Login>();
            //CreateMap<Holder, Card>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion

            #region Model to DTO
            CreateMap<Car, ResponseCarDTO>();
            CreateMap<CarImage, CarImageDTO>();
            CreateMap<Customer, ResponseCustomerDTO>();
            CreateMap<Employee, ResponseEmployeeDTO>();
            CreateMap<Owner, ResponseOwnerDTO>();
            CreateMap<Rental, ResponseRentalDTO>();
            CreateMap<Document, DocumentDTO>();
            CreateMap<Login, LoginRequest>();
            CreateMap<Login, LoginDTO>();
            //CreateMap<Card, Holder>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
            #endregion
        }
    }
}
