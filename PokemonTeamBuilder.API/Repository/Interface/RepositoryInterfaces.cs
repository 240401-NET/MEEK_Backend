using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Repository;
public interface IPKMTeamRepo{
    // Exposed
    public Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID);
    public Task<PokemonTeam> GetTeam(int id);
    public Task<PokemonTeam> GetTeam(string name);
    public Task<PokemonTeam> CreateNewTeam(PokemonTeam team, int trainerID);
    public Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam);
    public Task<PokemonTeam> DeleteTeam(int id);
    // Not Exposed
    public Task<bool> DoesTeamExist(string name);
    public Task<bool> DoesTeamExist(int id);
}