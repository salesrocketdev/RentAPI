using System.ComponentModel.DataAnnotations;
using Rent.Core.Models;

namespace Rent.Domain.Entities
{
    public class Customer : BaseModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Name { get; set; }

        [Range(18, int.MaxValue, ErrorMessage = "A idade deve ser maior que 18 anos.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [DataType(DataType.EmailAddress, ErrorMessage = "E-mail em formato inválido.")]
        public string? Email { get; set; }
        public string? Address { get; set; }

        // Navigation properties
        public required Document Document { get; set; }
    }
}
