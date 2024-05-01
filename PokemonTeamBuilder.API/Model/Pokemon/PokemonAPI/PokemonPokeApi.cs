using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonPokeApi
{
    [JsonPropertyName("id")]
    public int Id { get; set; }    
    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;
    [JsonPropertyName("stats")]
    public virtual ICollection<PokemonBaseStat> PokemonBaseStats { get; set; } = new List<PokemonBaseStat>();
    [JsonPropertyName("sprites")]
    public virtual PokemonSprite? PokemonSprite { get; set; }
    
    public virtual ICollection<PokemonTeamMember> PokemonTeamMembers { get; set; } = new List<PokemonTeamMember>();
    [JsonPropertyName("abilities")]
    public virtual ICollection<PokemonAbility> Abilities { get; set; } = new List<PokemonAbility>();
    [JsonPropertyName("moves")]
    public virtual ICollection<PokemonMove> Moves { get; set; } = new List<PokemonMove>();
    [JsonPropertyName("types")]
    public virtual ICollection<PokemonType> Types { get; set; } = new List<PokemonType>();
}
