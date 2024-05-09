using System.Collections.ObjectModel;
using PokemonTeamBuilder.API.DTO;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Utilities;
using helperFile = PokemonTeamBuilder.Tests.Helpers.TestHelpers;

namespace PokemonTeamBuilder.Tests;

public class PKMTeamUtilitiesTest
{
    [Fact]
    public void PkmTMFromDTOReturnsCorrectTeamMember()
    {
        //Arrange
        TeamMemberDTO pikachuDTO = helperFile.pikachuDTO;
        PokemonTeamMember pikachuRealMember = helperFile.pikachuTeamMember;
        //Act
        PokemonTeamMember pikachuTestMember = PKMTeamUtilities.PkmTMFromDTO(pikachuDTO);

        //Assert
        Assert.Equal(pikachuRealMember.PkmApiId, pikachuTestMember.PkmApiId);
        Assert.Equal(pikachuRealMember.IsShiny, pikachuTestMember.IsShiny);
        Assert.Equal(pikachuRealMember.NickName, pikachuTestMember.NickName);
        Assert.Equal(pikachuRealMember.Level, pikachuTestMember.Level);
        Assert.Equal(pikachuRealMember.Nature, pikachuTestMember.Nature);
        Assert.Equal(pikachuRealMember.RosterOrder, pikachuTestMember.RosterOrder);
    }

    [Fact]
    public void MoveSetFromDTOReturnsCorrectMoveSet()
    {
        //Arrange
        PokemonMoveSetDTO movesetDTO = new()
        {
            Move1 = "move1",
            Move2 = "move2",
            Move3 = "move3",
            Move4 = "move4"
        };
        //Act
        PokemonMoveSet pkmMoveset = PKMTeamUtilities.MoveSetFromDTO(movesetDTO);

        //Assert
        Assert.Equal(movesetDTO.Move1, pkmMoveset.Move1);
        Assert.Equal(movesetDTO.Move2, pkmMoveset.Move2);
        Assert.Equal(movesetDTO.Move3, pkmMoveset.Move3);
        Assert.Equal(movesetDTO.Move4, pkmMoveset.Move4);        
    }

    [Fact]
    public void StatsFromDTOReturnsCorrectStats()
    {
        //Arrange
        List<PokemonStatDTO> statsDTO = [
            new(){
                Effort = 1,
                Individual = 2,
                Name = "hp"    
            },
            new(){
                Effort = 3,
                Individual = 4,
                Name = "attack"    
            },
            new(){
                Effort = 5,
                Individual = 6,
                Name = "defense"    
            }
        ];
        //Act
        var pkmStats = PKMTeamUtilities.StatsFromDTO(statsDTO);

        //Assert
        Assert.NotNull(pkmStats);
        Assert.Equal(3, pkmStats.Count);
        Assert.Collection(pkmStats, 
            stat => stat.Name.Equals("hp"),
            stat => stat.Name.Equals("attack"),
            stat => stat.Name.Equals("defense"));      
    }

    [Theory]
    [InlineData(26, 18, "hp")]
    [InlineData(23, 40, "attack")]
    [InlineData(17, 8, "defense")]
    [InlineData(60, 99, "special-attack")]
    [InlineData(2, 5, "special-defense")]
    [InlineData(9, 81, "speed")]
    public void SingleStatFromDTOReturnsCorrectStats(int ev, int iv, string name)
    {
        //Arrange
        PokemonStatDTO statDTO = new()
        {
            Effort = ev,
            Individual = iv,
            Name = name
        };
        //Act
        var pkmStat = PKMTeamUtilities.SingleStatFromDTO(statDTO);

        //Assert
        Assert.NotNull(pkmStat);
        Assert.Equal(ev, pkmStat.Effort);    
        Assert.Equal(iv, pkmStat.Individual);    
        Assert.Equal(name, pkmStat.Name);    
    }

    [Theory]
    [InlineData(true, 4, "rare", "google.com")]
    [InlineData(false, 10, "common", "example.com")]
    [InlineData(true, 7, "uncommon", "stackoverflow.com")]
    [InlineData(false, 2, "rare", "github.com")]
    [InlineData(true, 5, "common", "wikipedia.org")]
    [InlineData(true, 8, "uncommon", "reddit.com")]
    
    public void AbilityFromDTOReturnsCorrectAbility(bool hidden, int slot, string name, string url)
    {
        //Arrange
        PokemonAbilityDTO abilityDTO = new()
        {
            IsHidden = hidden,
            Slot = slot,
            Name = name,
            Url = url
        };
        //Act
        var pkmAbility = PKMTeamUtilities.AbilityFromDTO(abilityDTO);

        //Assert
        Assert.NotNull(pkmAbility);
        Assert.Equal(hidden, pkmAbility.IsHidden);  
        Assert.Equal(slot, pkmAbility.Slot);  
        Assert.Equal(name, pkmAbility.Name);  
        Assert.Equal(url, pkmAbility.Url);  
    }
}