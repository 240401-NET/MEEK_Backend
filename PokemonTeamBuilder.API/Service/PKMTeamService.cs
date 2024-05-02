using Microsoft.IdentityModel.Tokens;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.Exceptoins;

namespace PokemonTeamBuilder.API.Service;

public class PKMTeamServices : IPKMTeamService
{
    private readonly IPKMTeamRepo _pkmTeamRepo;
    public PKMTeamServices(IPKMTeamRepo pkmTeamRepo) => _pkmTeamRepo = pkmTeamRepo;

    public Task<PokemonTeam> CreateNewTeam(PokemonTeam pkmTeam) => 
        DoesTeamExist(pkmTeam.Id).Result ? throw new ObjectExistException("This team is already in our database, please update the team.") : _pkmTeamRepo.CreateNewTeam(pkmTeam);

    public Task<PokemonTeam> DeleteTeam(int id) => 
        DoesTeamExist(id).Result ? _pkmTeamRepo.DeleteTeam(id) : throw new NullReferenceException("The team does not exist in the database.");
    

    public Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID)
    {
        Task<IEnumerable<PokemonTeam?>> returnList = _pkmTeamRepo.GetAll(trainerID);
        return returnList.Result.ToList().IsNullOrEmpty() ? throw new EmptyListException("The list is empty.") : returnList;
    }

    public Task<PokemonTeam> GetTeam(int id) => 
        DoesTeamExist(id).Result ? _pkmTeamRepo.GetTeam(id) : throw new NullReferenceException("The team does not exist in the database.");

    public Task<PokemonTeam> GetTeam(string name) =>
        DoesTeamExist(name).Result ? _pkmTeamRepo.GetTeam(name) : throw new NullReferenceException("The team does not exist in the database.");

    public Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam) => 
        DoesTeamExist(pkmTeam.Id).Result ? _pkmTeamRepo.UpdateTeam(pkmTeam) : throw new NullReferenceException("The team does not exist in the database.");

    private Task<bool> DoesTeamExist(string name) => _pkmTeamRepo.DoesTeamExist(name);
    private Task<bool> DoesTeamExist(int id) => _pkmTeamRepo.DoesTeamExist(id);
}
