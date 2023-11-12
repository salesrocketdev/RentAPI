using System.ComponentModel.DataAnnotations;
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

        [Required(ErrorMessage = "Informe a data inicial do aluguel.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Informe a data final do aluguel.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Informe o valor do deposito de segurança.")]
        public decimal SecurityDeposit { get; set; }

        // Navigation properties
        public Car? Car { get; set; }
        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
    }
}
