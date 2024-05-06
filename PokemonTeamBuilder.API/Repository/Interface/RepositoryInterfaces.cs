using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Repository;
public interface IPKMTeamRepo{
    // Exposed
    IEnumerable<PokemonTeam> GetAll(int trainerID);
    List<int> GetAllTeamId(int trainerId);
    Task<PokemonTeam> GetTeam(int id);
    Task<PokemonTeam> GetTeam(string name);
    PokemonTeam CreateNewTeam(PokemonTeam pkmTeam);
    PokemonTeam UpdateTeam(PokemonTeam pkmTeam);
    PokemonTeam DeleteTeam(int id);
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
    PokemonPokeApi? GetPkmFromDB(int id);
    PokemonPokeApi? GetPkmFromDB(string name);
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