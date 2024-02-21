using Rent.Domain.DTO.Response;

namespace Rent.Domain.DTO.Request.Create
{
    public class CreateCustomerDTO
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public CreateDocumentDTO? Document { get; set; }
    }
}
