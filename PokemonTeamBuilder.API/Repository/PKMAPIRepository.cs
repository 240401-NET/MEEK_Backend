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
        PokemonPokeApi tempPKM = new();
        tempPKM.Name = newPkm.Name;     
        
        //_pkmContext.PokemonPokeApis.Add(newPkm);
        //_pkmContext.PokemonPokeApis.Add(tempPKM);
        _pkmContext.PokemonBaseStats.Add(newPkm.PokemonBaseStats.First());
        _pkmContext.SaveChanges();
        //return  _pkmContext.PokemonPokeApis.Find(newPkm.Id)!;
    }
}