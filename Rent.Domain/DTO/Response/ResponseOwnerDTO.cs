using Rent.Core.Models;

namespace Rent.Domain.DTO.Response
{
    public class ResponseOwnerDTO : BaseDTO
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
