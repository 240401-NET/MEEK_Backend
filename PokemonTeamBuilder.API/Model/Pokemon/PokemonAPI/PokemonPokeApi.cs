using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonPokeApi
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<PokemonBaseStat> PokemonBaseStats { get; set; } = new List<PokemonBaseStat>();

    public virtual PokemonSprite? PokemonSprite { get; set; }

    public virtual ICollection<PokemonTeamMember> PokemonTeamMembers { get; set; } = new List<PokemonTeamMember>();

    public virtual ICollection<PokemonAbility> Abilities { get; set; } = new List<PokemonAbility>();

    public virtual ICollection<PokemonMove> Moves { get; set; } = new List<PokemonMove>();

    public virtual ICollection<PokemonType> Types { get; set; } = new List<PokemonType>();
}
