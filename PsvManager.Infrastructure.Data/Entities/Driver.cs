namespace PsvManager.Infrastructure.Data.Entities
{
    public class Driver
    {
        public Guid Id { get; set; }
        public string Forename { get; set; }
        public string Surname { get; set;}
        public string LicenseNumber { get; set; }
        public Guid AddressId { get; set; }
        public Address? Address { get; set; }
    }
}
