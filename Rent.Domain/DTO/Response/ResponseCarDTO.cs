using Rent.Core.Models;
using Rent.Domain.Enums;

namespace Rent.Domain.DTO.Response
{
    public class ResponseCarDTO : BaseDTO
    {
        public string? Model { get; set; }
        public int Year { get; set; }
        public string? Color { get; set; }
        public int SeatsNumber { get; set; }
        public string? Plate { get; set; }
        public decimal DailyValue { get; set; }
        public bool Available { get; set; }
        public FuelType FuelType { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public ResponseBrandDTO? Brand { get; set; }
        public List<CarImageDTO>? CarImages { get; set; }
    }
}
