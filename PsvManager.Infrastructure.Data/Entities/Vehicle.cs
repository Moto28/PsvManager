namespace PsvManager.Infrastructure.Data.Entities
{
    public class Vehicle
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Registration { get; set; }
        public int MaxPassengers { get; set; }
    }
}
