using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonTeam
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    [JsonIgnore]
    public int TrainerId { get; set; }
    [JsonIgnore]
    public virtual Trainer Trainer { get; set; } = null!;
    public virtual ICollection<PokemonTeamMember> PokemonTeamMembers { get; set; } = [];
}
