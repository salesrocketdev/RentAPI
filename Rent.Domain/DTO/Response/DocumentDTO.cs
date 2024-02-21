namespace Rent.Domain.DTO.Response
{
    public class DocumentDTO
    {
        public int CustomerId { get; set; }
        public string? TaxNumber { get; set; }
        public string? RG { get; set; }
        public string? DriverLicenseNumber { get; set; }
    }
}
