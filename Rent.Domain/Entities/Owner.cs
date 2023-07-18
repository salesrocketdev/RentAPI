using Rent.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Rent.Domain.Entities
{
    public class Owner : BaseModel
    {
        [Required(ErrorMessage = "O nome é obrigatório.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        public string? Email { get; set; }

        // Navigation properties
        public int LoginId { get; set; }
        public Login? Login { get; set; }
        public ICollection<Role>? Roles { get; set; }
    }
}
