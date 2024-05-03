using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Repository;

public class PKMAPIRepository : IPKMAPIRepository
{
    private readonly PokemonTrainerDbContext _pkmContext;

    public PKMAPIRepository(PokemonTrainerDbContext pkmContext) => _pkmContext = pkmContext;

    public PokemonPokeApi? GetPkmByIdFromDB(int id)
    {
        var pokemon = _pkmContext.PokemonPokeApis
        .Include(pkm => pkm.PokemonBaseStats)
        .Include(pkm => pkm.PokemonSprite)
        .Include(pkm => pkm.PokemonTeamMembers)
        .Include(pkm => pkm.Abilities)
        .Include(pkm => pkm.Moves)
        .Include(pkm => pkm.Types)
        .FirstOrDefault(pkm => pkm.Id == id);
        
        return pokemon;
    }

    public PokemonPokeApi? GetPkmByNameFromDB(string name)
    {
        var pkmId = _pkmContext.PokemonPokeApis
        .Where(pkm => pkm.Name.Equals(name))?
        .FirstOrDefault()?
        .Id;

        if(pkmId is not null)
        {
            var pokemon = GetPkmByIdFromDB((int)pkmId);
            return pokemon;
        }

        return null;
    }

    public void CreateNewPkmOnDB(PokemonPokeApi newPkm)
    {        
        _pkmContext.PokemonPokeApis.Add(newPkm);
        _pkmContext.SaveChanges();
    }
}