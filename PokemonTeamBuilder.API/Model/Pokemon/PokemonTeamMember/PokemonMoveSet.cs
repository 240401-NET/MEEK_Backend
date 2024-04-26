using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonMoveSet
{
    public string Move1 { get; set; } = null!;

    public string Move2 { get; set; } = null!;

    public string Move3 { get; set; } = null!;

    public string Move4 { get; set; } = null!;

    public int PkmTmId { get; set; }

    public virtual PokemonTeamMember PkmTm { get; set; } = null!;
}
