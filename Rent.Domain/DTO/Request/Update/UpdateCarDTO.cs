namespace Rent.Domain.DTO.Request.Update
{
    public class UpdateCarDTO
    {
        public int Id { get; set; }
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? Plate { get; set; }
        public decimal DailyValue { get; set; }
        public bool Available { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}
