using System.ComponentModel.DataAnnotations;
using Rent.Core.Models;

namespace Rent.Domain.Entities
{
    public class RevokedToken : BaseModel
    {
        [Required]
        public string? TokenId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
