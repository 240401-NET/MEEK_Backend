using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Repository;
public class PKMTeamRepository : IPKMTeamRepo
{
    public readonly PokemonTrainerDbContext _context;

    public PKMTeamRepository(PokemonTrainerDbContext context) => _context = context;

    public IEnumerable<PokemonTeam?> GetAll(int trainerID) => _context.PokemonTeams.Where(p => p.TrainerId == trainerID).ToList();   
}