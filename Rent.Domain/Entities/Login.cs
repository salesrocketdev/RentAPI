using Rent.Core.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent.Domain.Entities
{
    public class Login : BaseModel
    {
        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string? Password { get; set; }
    }
}
