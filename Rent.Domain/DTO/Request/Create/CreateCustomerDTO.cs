namespace Rent.Domain.DTO.Request.Create
{
    public class CreateCustomerDTO
    {
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PasswordConfirm { get; set; }
        public required CreateDocumentDTO Document { get; set; }

        public bool IsPasswordMatching()
        {
            return Password == PasswordConfirm;
        }
    }
}
