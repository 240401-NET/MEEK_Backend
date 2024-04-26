using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API;

public partial class PokemonSprite
{
    public string FrontDefault { get; set; } = null!;

    public string FrontShiny { get; set; } = null!;

    public string FrontFemale { get; set; } = null!;

    public string FrontShinyFemale { get; set; } = null!;

    public int PokemonId { get; set; }

    public virtual PokemonPokeApi Pokemon { get; set; } = null!;
}
