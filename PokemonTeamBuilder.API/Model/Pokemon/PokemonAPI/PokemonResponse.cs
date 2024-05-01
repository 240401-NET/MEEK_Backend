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
