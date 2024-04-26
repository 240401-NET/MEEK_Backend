using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonAbility
{
    public int Id { get; set; }

    public bool IsHidden { get; set; }

    public int Slot { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<PokemonTeamMember> PokemonTeamMembers { get; set; } = new List<PokemonTeamMember>();

    public virtual ICollection<PokemonPokeApi> PkmApis { get; set; } = new List<PokemonPokeApi>();
}
