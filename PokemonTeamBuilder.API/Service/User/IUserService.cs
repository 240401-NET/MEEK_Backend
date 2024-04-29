using Microsoft.AspNetCore.Identity;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Service;

public interface IUserService
{
    Task<IdentityResult> RegisterUser(RegisterUser registration);
    Task<SignInResult> LoginUser(LoginUser login, bool? useCookies, bool? useSessionCookies);
    void Logout();
}