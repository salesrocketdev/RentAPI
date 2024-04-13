using System.ComponentModel.DataAnnotations;
using Rent.Core.Models;

namespace Rent.Domain.Entities
{
    public class Owner : BaseModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Name { get; set; }

        [Range(18, int.MaxValue, ErrorMessage = "A idade deve ser maior que 18 anos.")]
        public int Age { get; set; }

        [Required(ErrorMessage = "O endereço é obrigatório.")]
        public string? Address { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório.")]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        public string? Email { get; set; }
    }
}
