namespace Rent.Domain.Entities
{
    public class RevokedToken
    {
        public int Id { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
