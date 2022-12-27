namespace Travel.Route.Domain.Entities
{
    public class TravelRoute : EntityBase
    {
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; private set; }
        public int? NumberOfStops { get; private set; }

        public void Inactivate()
        {
            Active = false;
        }

        public void SetPrice(decimal price)
        {
            Price = price;  
        }

        public void SetNumberOfStops(int? numberOfStops)
        {
            NumberOfStops = numberOfStops;  
        }
    }
}
