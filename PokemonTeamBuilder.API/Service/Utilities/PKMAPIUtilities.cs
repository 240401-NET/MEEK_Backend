using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.VisualBasic;

namespace PokemonTeamBuilder.API.Model.Utilities;

public static class PKMAPIUtilities
{
    private static JsonNode? _pkmJson;

    private static void SetPkmJsonDoc(JsonNode pkmJson)
    {
        _pkmJson = pkmJson;
    }

    public static PokemonPokeApi PokemonFromJson(JsonNode pkmJson)
    {        
        SetPkmJsonDoc(pkmJson);
        
        if(_pkmJson is null) 
            throw new ArgumentNullException("Pokemon Json doc was null");

        PokemonPokeApi pokemon = new();

        pokemon.Id = GetPkmId();
        pokemon.Name = GetPkmName();
        pokemon.PokemonBaseStats = GetPkmStats();
        pokemon.PokemonSprite = GetPkmSprites();
        //pokemon.PokemonTeamMembers ???? Why does pokeAPI have this field?
        pokemon.Abilities = GetPkmAbilities();
        pokemon.Moves = GetPkmMoves();
        pokemon.Types = GetPkmTypes();


        
        return pokemon;
    }

    public static int GetPkmId()
    {
        return (int)_pkmJson!["id"]!;
    }

    public static string GetPkmName()
    {
        return _pkmJson!["name"]!.ToString();
    }
    
    public static ICollection<PokemonBaseStat> GetPkmStats()
    {
        JsonArray statsJsonArray = _pkmJson!["stats"]!.AsArray();
        ICollection<PokemonBaseStat> baseStats = [];

        for(int i = 0; i < statsJsonArray.Count; i++)
        {
            baseStats.Add(new PokemonBaseStat
                {
                    //Id = i,
                    Name = statsJsonArray[i]!["stat"]!["name"]!.ToString(),
                    BaseStat = (int)statsJsonArray[i]!["base_stat"]!,
                    Url = statsJsonArray[i]!["stat"]!["url"]!.ToString(),
                    PkmApiId = GetPkmId()                    
                }
            );
        }
        
        return baseStats;
    }

    public static PokemonSprite GetPkmSprites()
    {
        JsonNode spritesJson = _pkmJson!["sprites"]!;
        PokemonSprite pkmSprites = JsonSerializer.Deserialize<PokemonSprite>(spritesJson)!;
        pkmSprites.PkmApiId = GetPkmId();
        return pkmSprites;
    }

    public static ICollection<PokemonAbility> GetPkmAbilities()
    {
        JsonArray abilitiesJsonArray = _pkmJson!["abilities"]!.AsArray();
        ICollection<PokemonAbility> abilities = [];

        for(int i = 0; i < abilitiesJsonArray.Count; i++)
        {
            abilities.Add(new PokemonAbility
                {
                    //Id = i,
                    IsHidden = (bool)abilitiesJsonArray[i]!["is_hidden"]!,
                    Slot = (int)abilitiesJsonArray[i]!["slot"]!,
                    Name = abilitiesJsonArray[i]!["ability"]!["name"]!.ToString(),
                    Url = abilitiesJsonArray[i]!["ability"]!["url"]!.ToString()                   
                }
            );
        }

        return abilities;
    }

    public static ICollection<PokemonMove> GetPkmMoves()
    {
        JsonArray movesJsonArray = _pkmJson!["moves"]!.AsArray();
        ICollection<PokemonMove> moves = [];

        for(int i = 0; i < movesJsonArray.Count; i++)
        {
            moves.Add(new PokemonMove
                {
                    //Id = i,
                    Name = movesJsonArray[i]!["move"]!["name"]!.ToString(),
                    Url = movesJsonArray[i]!["move"]!["url"]!.ToString()
                }
            );
        }

        return moves;
    }

    public static ICollection<PokemonType> GetPkmTypes()
    {
        JsonArray typesJsonArray = _pkmJson!["types"]!.AsArray();
        ICollection<PokemonType> types = [];

        for(int i = 0; i < typesJsonArray.Count; i++)
        {
            types.Add(new PokemonType
                {
                    //Id = i,
                    Slot = (int)typesJsonArray[i]!["slot"]!,
                    Url = typesJsonArray[i]!["type"]!["url"]!.ToString(),
                    Name = typesJsonArray[i]!["type"]!["name"]!.ToString(),
                }
            );
        }

        return types;
    }
}