namespace PsvManager.Infrastructure.Data.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string HouseNumber { get; set; }
        public string StreetName { get; set; }
        public string TownOrCity { get; set; }
        public string? County { get; set; }
        public string Postcode { get; set; }
    }
}
