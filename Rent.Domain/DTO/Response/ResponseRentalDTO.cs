using Rent.Core.Models;

namespace Rent.Domain.DTO.Response;

public class ResponseRentalDTO : BaseDTO
{
    public int CarId { get; set; }
    public int EmployeeId { get; set; }
    public int CustomerId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal SecurityDeposit { get; set; }

    // Navigation properties
    public ResponseCarDTO? Car { get; set; }
    public ResponseEmployeeDTO? Employee { get; set; }
    public ResponseCustomerDTO? Customer { get; set; }
}
