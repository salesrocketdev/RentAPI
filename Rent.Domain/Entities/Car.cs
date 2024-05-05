using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rent.Core.Models;
using Rent.Domain.Enums;

namespace Rent.Domain.Entities
{
    public class Car : BaseModel
    {
        [Required(ErrorMessage = "O modelo do carro é obrigatório.")]
        public string? Model { get; set; }

        [Required(ErrorMessage = "O id da marca é obrigatório.")]
        [ForeignKey("Brand")]
        public int BrandId { get; set; }

        [Range(1900, 2024, ErrorMessage = "O ano do carro deve estar entre 1900 e 2024.")]
        public int Year { get; set; }

        [Required(ErrorMessage = "A cor do carro é obrigatória.")]
        public string? Color { get; set; }

        [Required(ErrorMessage = "A quantidade de assentos é obrigatória.")]
        public int SeatsNumber { get; set; }

        [Required(ErrorMessage = "O placa do carro é obrigatória.")]
        public string? Plate { get; set; }

        [Required(ErrorMessage = "O valor da diária do carro é obrigatório.")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DailyValue { get; set; }

        [Required(ErrorMessage = "A disponibilidade do carro é obrigatória.")]
        public bool Available { get; set; }

        [Required(ErrorMessage = "O TransmissionType é obrigatório.")]
        public TransmissionType TransmissionType { get; set; }

        [Required(ErrorMessage = "O FuelType é obrigatório.")]
        public FuelType FuelType { get; set; }

        // Navigation properties
        public virtual Brand? Brand { get; set; }

        public List<CarImage?>? CarImages { get; set; }
    }
}
