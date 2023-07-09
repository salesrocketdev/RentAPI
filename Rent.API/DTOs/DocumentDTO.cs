namespace Rent.API.DTOs
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string? TaxNumber { get; set; }
        public string? RG { get; set; }
        public string? DriverLicenseNumber { get; set; }
    }
}
