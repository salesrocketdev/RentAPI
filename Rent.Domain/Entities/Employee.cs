using Rent.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Rent.Domain.Entities
{
    public class Employee : BaseModel
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

        // Navigation properties
        public int LoginId { get; set; }
        public Login? Login { get; set; }
        public ICollection<Role>? Roles { get; set; }
    }
}
