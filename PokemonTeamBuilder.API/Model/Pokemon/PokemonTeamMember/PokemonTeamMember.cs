using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonTeamMember
{
    public int Id { get; set; }

    public int PkmApiId { get; set; }

    public string NickName { get; set; } = null!;

    public int Level { get; set; }

    public int ChosenAbilityId { get; set; }

    public bool Gender { get; set; }

    public bool IsShiny { get; set; }

    public string TeraType { get; set; } = null!;

    public int HeldItemId { get; set; }

    public int PokemonTeamId { get; set; }

    public int RosterOrder { get; set; }

    public string? Nature { get; set; }

    public virtual PokemonAbility ChosenAbility { get; set; } = null!;

    public virtual ItemPokeApi HeldItem { get; set; } = null!;

    public virtual PokemonPokeApi PkmApi { get; set; } = null!;

    public virtual PokemonMoveSet? PokemonMoveSet { get; set; }

    public virtual ICollection<PokemonStat> PokemonStats { get; set; } = new List<PokemonStat>();

    public virtual PokemonTeam? PokemonTeam { get; set; } = null!;
}
