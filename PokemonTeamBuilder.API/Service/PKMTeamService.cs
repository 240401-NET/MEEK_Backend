using Microsoft.IdentityModel.Tokens;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.Exceptoins;

namespace PokemonTeamBuilder.API.Service;

public class PKMTeamServices : IPKMTeamService
{
    private readonly IPKMTeamRepo _pkmTeamRepo;
    public PKMTeamServices(IPKMTeamRepo pkmTeamRepo) => _pkmTeamRepo = pkmTeamRepo;

    public Task<PokemonTeam> CreateNewTeam(PokemonTeam team, int trainerID)
    {
        return _pkmTeamRepo.CreateNewTeam(team, trainerID);
    }

    public Task<PokemonTeam> DeleteTeam(int id)
    {
        return _pkmTeamRepo.DeleteTeam(id);
    }

    public Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID)
    {
        Task<IEnumerable<PokemonTeam?>> returnList = _pkmTeamRepo.GetAll(trainerID);
        return returnList.Result.ToList().IsNullOrEmpty() ? throw new EmptyListException() : returnList;
    }

    public Task<PokemonTeam> GetTeam(int id)
    {
        return _pkmTeamRepo.GetTeam(id);
    }

    public Task<PokemonTeam> GetTeam(string name)
    {
        return _pkmTeamRepo.GetTeam(name);
    }

    public Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam)
    {
        return _pkmTeamRepo.UpdateTeam(pkmTeam);
    }

    private Task<bool> DoesTeamExist(string name) => _pkmTeamRepo.DoesTeamExist(name);
    private Task<bool> DoesTeamExist(int id) => _pkmTeamRepo.DoesTeamExist(id);
}
