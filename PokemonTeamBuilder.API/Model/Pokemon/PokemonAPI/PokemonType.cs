using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API;

public partial class PokemonType
{
    public int Id { get; set; }

    public int Slot { get; set; }

    public string Url { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<PokemonPokeApi> PkmApis { get; set; } = new List<PokemonPokeApi>();
}
