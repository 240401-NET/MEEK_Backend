using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonStat
{
    [JsonIgnore]
    public int Id { get; set; }
    public int Effort { get; set; }
    public int Individual { get; set; }
    public string Name { get; set; } = null!;
    public string? Url { get; set; } = null!;
    public int PkmTmId { get; set; }
    public int? Total { get; set; }

    [JsonIgnore]
    public virtual PokemonTeamMember PkmTm { get; set; } = null!;
}
