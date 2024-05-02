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
        throw new NotImplementedException();
    }

    public Task<PokemonTeam> DeleteTeam(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID)
    {
        Task<IEnumerable<PokemonTeam?>> returnList = _pkmTeamRepo.GetAll(trainerID);
        return returnList.Result.ToList().IsNullOrEmpty() ? throw new EmptyListException() : returnList;
    }

    public Task<PokemonTeam> GetTeam(int id)
    {
        throw new NotImplementedException();
    }

    public Task<PokemonTeam> GetTeam(string name)
    {
        throw new NotImplementedException();
    }

    public Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam)
    {
        throw new NotImplementedException();
    }
}
