using PokemonTeamBuilder.API.Model;
namespace PokemonTeamBuilder.API.Repository;
public interface IPKMTeamRepo{
    public IEnumerable<PokemonTeam?> GetAll(int trainerID);
    // add declaration here

}