using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Repository;

public class PKMAPIRepository : IPKMAPIRepository
{
    private readonly PokemonTrainerDbContext _pkmContext;

    public PKMAPIRepository(PokemonTrainerDbContext pkmContext) => _pkmContext = pkmContext;

    public PokemonPokeApi? GetPkmByIdFromDB(int id)
    {
        return _pkmContext.PokemonPokeApis.FirstOrDefault(pkm => pkm.Id == id);
    }

    public PokemonPokeApi? GetPkmByNameFromDB(string name)
    {
        return _pkmContext.PokemonPokeApis.FirstOrDefault(pkm => pkm.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
    }

    public void CreateNewPkmOnDB(PokemonPokeApi newPkm)
    {        
        _pkmContext.PokemonPokeApis.Add(newPkm);
        _pkmContext.SaveChanges();
        //return  _pkmContext.PokemonPokeApis.Find(newPkm.Id)!;
    }
}