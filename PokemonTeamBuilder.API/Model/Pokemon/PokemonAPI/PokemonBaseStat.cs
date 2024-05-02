using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace PokemonTeamBuilder.API.Model;

public partial class PokemonBaseStat
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int BaseStat { get; set; }
    public string Url { get; set; } = null!;
    public int PkmApiId { get; set; }

    public virtual PokemonPokeApi PkmApi { get; set; } = null!;
}
