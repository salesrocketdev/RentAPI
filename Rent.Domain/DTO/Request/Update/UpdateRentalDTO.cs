namespace Rent.Domain.DTO.Request.Update;

public class UpdateRentalDTO
{
    public int Id { get; set; }
    public int CarId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
