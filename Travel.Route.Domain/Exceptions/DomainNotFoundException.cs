namespace Travel.Route.Domain.Exceptions
{
    public class DomainNotFoundException : Exception
    {
        public DomainNotFoundException()
        {
        }

        public DomainNotFoundException(string message)
            : base(message ?? string.Empty)
        {
        }
    }
}
