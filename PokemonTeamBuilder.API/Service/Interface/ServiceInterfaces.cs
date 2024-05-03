using Microsoft.AspNetCore.Identity;
using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Service;

public interface IPKMTeamService{
    public Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID);
    public Task<PokemonTeam> GetTeam(int id);
    public Task<PokemonTeam> GetTeam(string name);
    public Task<PokemonTeam> CreateNewTeam(PokemonTeam team);
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
    Task<IEnumerable<PokemonNameandURL>> GetAllPokemon();
    Task<PokemonPokeApi> GetPokemonById(int pokemonId);
    Task<PokemonPokeApi> GetPokemonByName(string pokemonName);
}

public interface IPTMService
{
    public PokemonTeam AddPkmToTeam(PokemonTeamMember newPKM, int teamId);
    public PokemonTeamMember UpdatePTM(PokemonTeamMember updatedPKM);
    public IEnumerable<PokemonTeamMember> GetAllPTMByTeamId(int teamId);
    
}