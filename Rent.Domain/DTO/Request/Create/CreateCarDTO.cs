using Rent.Domain.DTO.Response;

namespace Rent.Domain.DTO.Request.Create
{
    public class CreateCarDTO
    {
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? Plate { get; set; }
        public decimal DailyValue { get; set; }
        public bool Available { get; set; }
        public int BrandId { get; set; }
    }
}
