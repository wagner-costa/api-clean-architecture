namespace Travel.Route.Domain.Models
{
    public class TokenModel
    {
        public string? AccessToken { get; set; }

        public double ExpiresIn { get; set; }
    }
}
