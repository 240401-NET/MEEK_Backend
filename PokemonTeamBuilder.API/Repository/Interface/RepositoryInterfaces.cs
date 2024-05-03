using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Repository;
public interface IPKMTeamRepo{
    // Exposed
    public Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID);
    public Task<PokemonTeam> GetTeam(int id);
    public Task<PokemonTeam> GetTeam(string name);
    public Task<PokemonTeam> CreateNewTeam(PokemonTeam pkmTeam);
    public Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam);
    public Task<PokemonTeam> DeleteTeam(int id);
    // Not Exposed
    public Task<bool> DoesTeamExist(string name);
    public Task<bool> DoesTeamExist(int id);

}

public interface IPTMRepository
{
    PokemonTeamMember GetPTMById(int id);
    PokemonTeam GetPkmTeamById(int id);
    IEnumerable<PokemonTeamMember> GetAllPTMByTeamId(int teamId);
    PokemonTeamMember UpdatePTM(PokemonTeamMember updatedPKM);
    PokemonTeam AddPkmToTeam(PokemonTeamMember newPKM, int teamId);
}

public interface IPKMAPIRepository
{
    PokemonPokeApi? GetPkmByIdFromDB(int id);
    PokemonPokeApi? GetPkmByNameFromDB(string name);
    void CreateNewPkmOnDB(PokemonPokeApi newPkm);    
    void CreateNewItemOnDB(ItemPokeApi newItem);
    ItemPokeApi? GetItemByIDFromDB(int id);
}