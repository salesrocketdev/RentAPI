using Rent.Core.Models;
using Rent.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Rent.Domain.Entities
{
    public class Login : BaseModel
    {
        [Required(ErrorMessage = "O ParentId é obrigatório.")]
        public int ParentId { get; set; }

        [Required(ErrorMessage = "O Email é obrigatório.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "A Senha é obrigatória.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "O UserType é obrigatório.")]
        public UserType UserType { get; set; }
    }
}
