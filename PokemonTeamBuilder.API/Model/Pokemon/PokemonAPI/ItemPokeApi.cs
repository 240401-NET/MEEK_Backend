using System;
using System.Collections.Generic;

namespace PokemonTeamBuilder.API;

public partial class ItemPokeApi
{
    public string Name { get; set; } = null!;

    public string Sprite { get; set; } = null!;

    public int Id { get; set; }

    public virtual ICollection<PokemonTeamMember> PokemonTeamMembers { get; set; } = new List<PokemonTeamMember>();
}
