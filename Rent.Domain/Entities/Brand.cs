using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Rent.Core.Models;

namespace Rent.Domain.Entities;

public class Brand : BaseModel
{
    [Required(ErrorMessage = "O nome da marca é obrigatório.")]
    public string? Name { get; set; }

    [Required(ErrorMessage = "O link da imagem da marca é obrigatório.")]
    public string? BrandImage { get; set; }

    [NotMapped]
    public int Quantity { get; set; }
}
