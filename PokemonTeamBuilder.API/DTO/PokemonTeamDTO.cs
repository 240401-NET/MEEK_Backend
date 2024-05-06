using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.DTO;

public record struct PokemonTeamDTO(int? Id, string Name, ICollection<TeamMemberDTO> PokemonTeamMembers);

public record struct TeamMemberDTO
(
    int PkmApiId,
    string? NickName,
    int Level,
    string ChosenAbility,
    bool Gender,
    bool IsShiny,
    string TeraType,
    string? HeldItem,
    int RosterOrder,
    string Nature,
    PokemonMoveSetDTO? PokemonMoveSet,
    ICollection<PokemonStatDTO> PokemonStats
);

public record struct PokemonAbilityDTO
(
    bool IsHidden,
    int Slot,
    string Name,
    string Url
);

public record struct PokemonMoveSetDTO
(
    string Move1,
    string Move2,
    string Move3,
    string Move4
);

public record struct PokemonStatDTO
(
    int Effort,
    int Individual,
    string Name
);
