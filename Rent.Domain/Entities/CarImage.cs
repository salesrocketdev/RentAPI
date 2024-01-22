using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rent.Core.Models;

namespace Rent.Domain.Entities
{
    public class CarImage : BaseModel
    {
        [ForeignKey("Car")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "A imagem é obrigatória.")]
        public string? Base64 { get; set; }
    }
}
