using Travel.Route.Domain.Contracts.Services;
using Travel.Route.Domain.Models;

namespace Travel.Route.Domain.Services
{
    public class AccountService : IAccountService
    {
        public Task<bool> HasPermission(string token, string verb, string route)
        {
            throw new System.NotImplementedException();
        }

        public Task<TokenModel> Login(LoginModel user)
        {
            throw new System.NotImplementedException();
        }
    }
}
