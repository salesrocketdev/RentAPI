using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rent.Core.Models;

namespace Rent.Domain.Entities
{
    public class Document : BaseModel
    {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        public string? TaxNumber { get; set; }

        // [Required(ErrorMessage = "O RG é obrigatório.")]
        public string? RG { get; set; }

        // [Required(ErrorMessage = "A CNH é obrigatória.")]
        public string? DriverLicenseNumber { get; set; }
    }
}
