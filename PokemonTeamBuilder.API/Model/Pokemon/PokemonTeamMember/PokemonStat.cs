using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonStat
{
    public int Effort { get; set; }

    public int Individual { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public int PkmTmId { get; set; }

    public int Total { get; set; }

    public int Id { get; set; }

    public virtual PokemonTeamMember? PkmTm { get; set; } = null!;
}
