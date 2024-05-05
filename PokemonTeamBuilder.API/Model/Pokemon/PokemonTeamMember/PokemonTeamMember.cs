using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonTeamMember
{
    public int Id { get; set; }
    public int PkmApiId { get; set; }
    public string? NickName { get; set; } = null;
    public int Level { get; set; } = 100;
    public string ChosenAbility { get; set; } = "";
    public bool Gender { get; set; } = true;
    public bool IsShiny { get; set; } = false;
    public string TeraType { get; set; } = null!;
    public string? HeldItem { get; set; } = "none";
    public int PokemonTeamId { get; set; }
    public int RosterOrder { get; set; } = 0;
    public string Nature { get; set; } = "Bashful";
    
    // [JsonIgnore]
    // public virtual ItemPokeApi? HeldItem { get; set; } = null;
    [JsonIgnore]
    public virtual PokemonPokeApi PkmApi { get; set; } = null!;
    
    public virtual PokemonMoveSet? PokemonMoveSet { get; set; }
    
    public virtual ICollection<PokemonStat> PokemonStats { get; set; } = [];
    [JsonIgnore]
    public virtual PokemonTeam? PokemonTeam { get; set; }
}
