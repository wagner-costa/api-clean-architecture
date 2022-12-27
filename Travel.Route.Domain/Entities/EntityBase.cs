namespace Travel.Route.Domain.Entities
{
    public class EntityBase
    {
        public EntityBase() => CreatedDate = DateTime.Now;

        public int Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public bool Active { get; set; }
    }
}