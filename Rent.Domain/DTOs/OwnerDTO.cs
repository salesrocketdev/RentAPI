using System.ComponentModel.DataAnnotations;

namespace Rent.Domain.DTOs
{
    public class OwnerDTO : Core.Models.BaseDTO
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
