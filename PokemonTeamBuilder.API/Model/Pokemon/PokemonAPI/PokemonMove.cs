using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonMove
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<PokemonPokeApi> PkmApis { get; set; } = new List<PokemonPokeApi>();
}
