namespace Rent.Domain.DTO.Response
{
    public class CarDTO
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? Plate { get; set; }
        public decimal DailyValue { get; set; }
        public bool Available { get; set; }
        public List<CarImageDTO>? CarImages { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
