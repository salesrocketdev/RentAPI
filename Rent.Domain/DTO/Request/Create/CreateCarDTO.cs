﻿namespace Rent.Domain.DTO.Request.Create
{
    public class CreateCarDTO
    {
        public string? Brand { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public string? Plate { get; set; }
        public decimal Value { get; set; }
        public bool Available { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
