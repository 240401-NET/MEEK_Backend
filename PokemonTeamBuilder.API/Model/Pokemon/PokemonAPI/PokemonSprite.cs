using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonSprite
{
    [JsonPropertyName("front_default")]
    public string FrontDefault { get; set; } = "";
    [JsonPropertyName("front_shiny")]
    public string FrontShiny { get; set; } = "";
    [JsonPropertyName("front_female")]
    public string FrontFemale { get; set; } = "";
    [JsonPropertyName("front_shiny_female")]
    public string FrontShinyFemale { get; set; } = "";
    
    public int PkmApiId { get; set; }

    [JsonIgnore]
    public virtual PokemonPokeApi? PkmApi { get; set; } = null!;
}
