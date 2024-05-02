using Microsoft.AspNetCore.Identity;
using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Service;

public interface IPKMTeamService{
    public Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID);
    public Task<PokemonTeam> GetTeam(int id);
    public Task<PokemonTeam> GetTeam(string name);
    public Task<PokemonTeam> CreateNewTeam(PokemonTeam team, int trainerID);
    public Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam);
    public Task<PokemonTeam> DeleteTeam(int id);

}

public interface IUserService
{
    Task<IdentityResult> RegisterUser(RegisterUser registration);
    Task<SignInResult> LoginUser(LoginUser login, bool? useCookies, bool? useSessionCookies);
    void Logout();
}

public interface IPKMAPISevice
{
    
}