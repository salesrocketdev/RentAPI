using System.ComponentModel.DataAnnotations;
using Rent.Core.Models;

namespace Rent.Domain.Entities;

public class Brand : BaseModel
{
    [Required(ErrorMessage = "O nome da marca é obrigatório.")]
    public string? Name { get; set; }
}
