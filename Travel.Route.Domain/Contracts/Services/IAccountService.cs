using Travel.Route.Domain.Models;

namespace Travel.Route.Domain.Contracts.Services
{
    public interface IAccountService
    {
        Task<TokenModel> Login(LoginModel user);

        Task<bool> HasPermission(string token, string verb, string route);
    }
}
