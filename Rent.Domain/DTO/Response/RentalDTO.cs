namespace Rent.Domain.DTO.Response;

public class RentalDTO
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public CarDTO? Car { get; set; }
    public CustomerDTO? Customer { get; set; }
}
