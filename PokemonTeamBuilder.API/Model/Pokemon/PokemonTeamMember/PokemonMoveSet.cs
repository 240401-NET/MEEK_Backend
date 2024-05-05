using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonMoveSet
{
    [JsonIgnore]
    public int Id { get; set; }    
    public string Move1 { get; set; } = null!;
    public string Move2 { get; set; } = null!;
    public string Move3 { get; set; } = null!;
    public string Move4 { get; set; } = null!;
    public int PkmTmId { get; set; }

    [JsonIgnore]
    public virtual PokemonTeamMember PkmTm { get; set; } = null!;
}
