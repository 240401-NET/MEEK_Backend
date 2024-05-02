using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Repository;
public interface IPKMTeamRepo{
    public IEnumerable<PokemonTeam?> GetAll(int trainerID);
    // add declaration here

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
}