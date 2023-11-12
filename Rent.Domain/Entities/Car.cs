using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rent.Core.Models;

namespace Rent.Domain.Entities
{
    public class Car : BaseModel
    {
        [Required(ErrorMessage = "A marca do carro é obrigatória.")]
        public string? Brand { get; set; }

        [Required(ErrorMessage = "O modelo do carro é obrigatório.")]
        public string? Model { get; set; }

        [Range(1900, 2024, ErrorMessage = "O ano do carro deve estar entre 1900 e 2024.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "A cor do carro é obrigatória.")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "O placa do carro é obrigatória.")]
        public string? Plate { get; set; }

        [Required(ErrorMessage = "O valor da diária do carro é obrigatório.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DailyValue { get; set; }

        public bool Available { get; set; }
    }
}
