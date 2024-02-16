using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent.Domain.Entities
{
    public class CarImage
    {
        public int Id { get; set; }

        [ForeignKey("Car")]
        public int CarId { get; set; }

        [Required(ErrorMessage = "O link da imagem é obrigatório.")]
        public string? Link { get; set; }

        public bool IsPrimary { get; set; }
    }
}
