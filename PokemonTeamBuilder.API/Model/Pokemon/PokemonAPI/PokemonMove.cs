using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API;

public partial class PokemonMove
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Url { get; set; } = null!;

    public virtual ICollection<PokemonPokeApi> PkmApis { get; set; } = new List<PokemonPokeApi>();
}
