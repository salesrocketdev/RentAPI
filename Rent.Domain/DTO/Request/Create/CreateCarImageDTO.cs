namespace Rent.Domain;

public class CreateCarImageDTO
{
    public int CarId { get; set; }
    public string? Image { get; set; }
    public bool IsPrimary { get; set; }
}
