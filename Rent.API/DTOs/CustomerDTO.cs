using System.ComponentModel.DataAnnotations;

namespace Rent.API.DTOs
{
  public class CustomerDTO
  {
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Age { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Email { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DocumentDTO? Document { get; set; }
    }
}
