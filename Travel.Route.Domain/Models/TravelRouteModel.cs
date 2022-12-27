namespace Travel.Route.Domain.Models
{
    public class TravelRouteModel
    {
        public int Id { get; set; }
        public string? Origin { get; set; }
        public string? Destination { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public int? NumberOfStops { get; set; }  
    }
}