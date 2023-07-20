namespace Rent.Domain.DTOs.Response
{
    public class EmployeeDTO : Core.Models.BaseDTO
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
    }
}
