using Microsoft.IdentityModel.Tokens;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.Exceptoins;

namespace PokemonTeamBuilder.API.Service;

public class PKMTeamServices : IPKMTeamService
{
    private readonly IPKMTeamRepo _pkmTeamRepo;
    public PKMTeamServices(IPKMTeamRepo pkmTeamRepo) => _pkmTeamRepo = pkmTeamRepo;
    public IEnumerable<PokemonTeam?> GetAll(int trainerID)
    {
        IEnumerable<PokemonTeam?> returnList = _pkmTeamRepo.GetAll(trainerID);
        return returnList.ToList().IsNullOrEmpty() ? throw new EmptyListException() : returnList;
    }
}
