using Rent.Core.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rent.Domain.Entities
{
    public class Rental : BaseModel
    {
        [ForeignKey("Car")]
        public int CarId { get; set; }

        [ForeignKey("Customer")]
        public int CustomerId { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Navigation properties
        public Car? Car { get; set; }
        public Customer? Customer { get; set; }
    }
}
