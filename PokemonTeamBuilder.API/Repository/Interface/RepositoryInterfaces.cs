using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Repository;
public interface IPKMTeamRepo{
    // Exposed
    IEnumerable<PokemonTeam> GetAll(int trainerID);
    Task<PokemonTeam> GetTeam(int id);
    Task<PokemonTeam> GetTeam(string name);
    PokemonTeam CreateNewTeam(PokemonTeam pkmTeam);
    Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam);
    Task<PokemonTeam> DeleteTeam(int id);
    // Not Exposed
    Task<bool> DoesTeamExist(string name);
    Task<bool> DoesTeamExist(int id);

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

public interface ITrainerRepository
{
    Trainer CreateTrainer(string name);
    Trainer? GetTrainerById(int id);
    Trainer? GetTrainerByName(string name);
    void DeleteTrainer(Trainer trainer);
}

public interface IUserRepository
{
    void AddTrainerToUser(string username, Trainer trainer);
    ApplicationUser? GetUserByName(string username);
    int GetTrainerId(string username);
}