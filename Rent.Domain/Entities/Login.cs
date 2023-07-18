using Rent.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Rent.Domain.Entities
{
    public class Login : BaseModel
    {
        public int LoginId { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string? Password { get; set; }

        // Relacionamentos
        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
        public Owner? Owner { get; set; }
    }
}
