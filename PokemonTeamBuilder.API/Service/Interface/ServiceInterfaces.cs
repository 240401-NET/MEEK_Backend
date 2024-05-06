using Microsoft.AspNetCore.Identity;
using PokemonTeamBuilder.API.DTO;
using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Service;

public interface IPKMTeamService
{
    IEnumerable<PokemonTeam> GetAll(int trainerID);
    Task<PokemonTeam> GetTeam(int id);
    Task<PokemonTeam> GetTeam(string name);
    PokemonTeam CreateNewTeam(PokemonTeamDTO pkmTeam, int trainerId);
    PokemonTeam UpdateTeam(PokemonTeamDTO pkmTeam, int trainerId);
    Task<PokemonTeam> DeleteTeam(int id);

}

public interface IUserService
{
    void CreateTrainerForUser(string username);
    Task<IdentityResult> RegisterUser(RegisterUser registration);
    Task<SignInResult> LoginUser(LoginUser login);
    void Logout();
    ApplicationUser? GetUserByName(string username);
}

public interface IPKMAPISevice
{
    Task<IEnumerable<PokemonNameandURL>> GetAllPokemon();
    Task<PokemonPokeApi> GetPokemonById(int pokemonId);
    Task<PokemonPokeApi> GetPokemonByName(string pokemonName);
}

public interface IPTMService
{
    IEnumerable<PokemonTeamMember> GetAllPTMByTeamId(int teamId);
    PokemonTeamMember UpdatePTM(PokemonTeamMember updatedPKM);
    PokemonTeam AddPkmToTeam(PokemonTeamMember newPKM, int teamId);
    PokemonTeam DeletePTMFromTeam(PokemonTeamMember deletePKM);
    PokemonTeam DeleteAllPkmFromTeam(int teamId);
}

public interface ITrainerService
{
    Trainer CreateTrainer(string name);
    int GetTrainerIdByUsername(string username);
}