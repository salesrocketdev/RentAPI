namespace Rent.Domain.Entities
{
    public class Role
    {
        public int RoleId { get; set; }
        public string? Name { get; set; }

        // Relacionamentos
        public Customer? Customer { get; set; }
        public Employee? Employee { get; set; }
        public Owner? Owner { get; set; }
    }
}
