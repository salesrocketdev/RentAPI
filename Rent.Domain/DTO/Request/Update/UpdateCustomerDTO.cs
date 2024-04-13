using Rent.Domain.DTO.Response;

namespace Rent.Domain.DTO.Request.Update
{
    public class UpdateCustomerDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public required DocumentDTO Document { get; set; }
    }
}
