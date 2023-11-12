namespace Rent.Domain.DTO.Request.Create;

public class CreateRentalDTO
{
    public int CarId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public decimal SecurityDeposit { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
