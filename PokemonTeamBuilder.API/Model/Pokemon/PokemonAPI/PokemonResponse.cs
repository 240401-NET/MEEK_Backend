using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;
 
 public class PokemonNameandURL
{
    [JsonPropertyName("name")]
    public string? Name { get; set; }
    [JsonPropertyName("url")]
    public string? URL { get; set; }

    public override string ToString()
    {
        return $"Name: {Name}\n\tURL: {URL}";
    }
}

public class PokemonAPIResponse
{
    [JsonPropertyName("count")]
    public int Count { get; set; }
    [JsonPropertyName("next")]
    public string? Next { get; set; }
    [JsonPropertyName("previous")]
    public string? Previous { get; set; }
    [JsonPropertyName("results")]
    public List<PokemonNameandURL> Pokemons { get; set; } = [];

    public override string ToString()
    {
        string pokemonList = "";

        foreach (PokemonNameandURL pokemon in Pokemons)
        {
            pokemonList += "\t" + pokemon.ToString() + "\n";
        }
        
        return $"Count: {Count}\nNext: {Next}\nPrevious: {Previous}\nResults: \n{pokemonList}\n";
    }
}
