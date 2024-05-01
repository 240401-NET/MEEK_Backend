using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonSprite
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; } = null!;
    [JsonPropertyName("front_shiny")]
    public string FrontShiny { get; set; } = null!;
    [JsonPropertyName("front_female")]
    public string FrontFemale { get; set; } = null!;
    [JsonPropertyName("front_shiny_female")]
    public string FrontShinyFemale { get; set; } = null!;
    
    public int PkmApiId { get; set; }

    public virtual PokemonPokeApi PkmApi { get; set; } = null!;
}
