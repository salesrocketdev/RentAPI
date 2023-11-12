using System.ComponentModel.DataAnnotations.Schema;
using Rent.Core.Models;

namespace Rent.Domain.Entities
{
    public class Rental : BaseModel
    {
        [ForeignKey("Car")]
        public int CarId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal SecurityDeposit { get; set; }

        // Navigation properties
        public Car? Car { get; set; }
        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
    }
}
