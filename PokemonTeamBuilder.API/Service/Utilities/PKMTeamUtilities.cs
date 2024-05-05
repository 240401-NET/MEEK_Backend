using System.Collections.ObjectModel;
using PokemonTeamBuilder.API.DTO;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Utilities;

public static class PKMTeamUtilities
{
    public static PokemonTeamMember PkmTMFromDTO(TeamMemberDTO teamMemberDTO)
    {
        PokemonTeamMember newTeamMember = new()
        {
            PkmApiId = teamMemberDTO.PkmApiId,
            NickName = teamMemberDTO.NickName,
            Level = teamMemberDTO.Level,
            ChosenAbility = teamMemberDTO.ChosenAbility,
            Gender = teamMemberDTO.Gender,
            IsShiny = teamMemberDTO.IsShiny,
            TeraType = teamMemberDTO.TeraType,
            HeldItem = teamMemberDTO.HeldItem,
            RosterOrder = teamMemberDTO.RosterOrder,
            Nature = teamMemberDTO.Nature,
            PokemonMoveSet = MoveSetFromDTO(teamMemberDTO.PokemonMoveSet ?? new PokemonMoveSetDTO()),
            PokemonStats = StatsFromDTO(teamMemberDTO.PokemonStats)
        };

        return newTeamMember;
    }

    public static PokemonMoveSet MoveSetFromDTO(PokemonMoveSetDTO teamMoveDTO)
    {
        PokemonMoveSet newMoveSet = new()
        {
            Move1 = teamMoveDTO.Move1,
            Move2 = teamMoveDTO.Move2,
            Move3 = teamMoveDTO.Move3,
            Move4 = teamMoveDTO.Move4
        };
        return newMoveSet;
    }

    public static ICollection<PokemonStat> StatsFromDTO(ICollection<PokemonStatDTO> statsDTO)
    {
        ICollection<PokemonStat> newStats = [];

        foreach (PokemonStatDTO pokemonStatDTO in statsDTO)
        {
            newStats.Add(SingleStatFromDTO(pokemonStatDTO));
        }
        return newStats;
    }

    public static PokemonStat SingleStatFromDTO(PokemonStatDTO statDTO)
    {
        PokemonStat newStat = new()
        {
            Effort = statDTO.Effort,
            Individual = statDTO.Individual,
            Name = statDTO.Name
        };
        return newStat;
    }

    public static PokemonAbility AbilityFromDTO(PokemonAbilityDTO abilityDTO)
    {
        PokemonAbility newAbility = new()
        {
            IsHidden = abilityDTO.IsHidden,
            Slot = abilityDTO.Slot,
            Name = abilityDTO.Name,
            Url = abilityDTO.Url
        };
        return newAbility;
    }
}