using Microsoft.AspNetCore.Identity;
using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Service;

public interface IPKMTeamService{
    public IEnumerable<PokemonTeam?> GetAll(int trainerID);
}

public interface IUserService
{
    Task<IdentityResult> RegisterUser(RegisterUser registration);
    Task<SignInResult> LoginUser(LoginUser login, bool? useCookies, bool? useSessionCookies);
    void Logout();
}

public interface IPKMAPISevice
{
    Task<IEnumerable<PokemonNameandURL>> GetAllPokemon();
    Task<PokemonPokeApi> GetPokemonById(int pokemonId);
    Task<PokemonPokeApi> GetPokemonByName(string pokemonName);
    void TestMe();
}

public interface IPTMService
{

}