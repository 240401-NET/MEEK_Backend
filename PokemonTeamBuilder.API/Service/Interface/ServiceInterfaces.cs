using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Service;

public interface IPKMTeamService{
    public IEnumerable<PokemonTeam?> GetAll(int trainerID);
}

