using System.Collections.ObjectModel;
using System.Text.Json.Nodes;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Utilities;
using helperFile = PokemonTeamBuilder.Tests.Helpers.TestHelpers;

namespace PokemonTeamBuilder.Tests;

public class PKMAPIUtilitiesTest
{
    [Fact]
    public void PokemonFromJsonThrowExceptionWhenJsonIsNull()
    {
        //Arrange
        JsonNode pokemonJson = null!;    

        //Act

        //Assert
        Assert.Throws<ArgumentNullException>(() => PKMAPIUtilities.PokemonFromJson(pokemonJson));
    }

    [Fact]
    public void PokemonFromJsonReturnsCorrectPokemonWhenJsonIsGood()
    {
        //Arrange
        JsonNode pokemonJson = JsonNode.Parse(helperFile.PikachuJson)!;    

        //Act
        var pikachuFromJson = PKMAPIUtilities.PokemonFromJson(pokemonJson);

        //Assert
        Assert.NotNull(pikachuFromJson);
        Assert.IsType<PokemonPokeApi>(pikachuFromJson);
        Assert.Equal(25, pikachuFromJson.Id);
        Assert.Equal("pikachu", pikachuFromJson.Name);
    }

    [Fact]
    public void GetPkmIdReturnsCorrectPokemonId()
    {
        //Arrange
        JsonNode pokemonJson = JsonNode.Parse(helperFile.PikachuJson)!;
        var pikachuFromJson = PKMAPIUtilities.PokemonFromJson(pokemonJson);    

        //Act
        int pokemonId = PKMAPIUtilities.GetPkmId();

        //Assert
        Assert.Equal(25, pokemonId);
    }

    [Fact]
    public void GetPkmNameReturnsCorrectPokemonName()
    {
        //Arrange
        JsonNode pokemonJson = JsonNode.Parse(helperFile.PikachuJson)!;
        var pikachuFromJson = PKMAPIUtilities.PokemonFromJson(pokemonJson);    

        //Act
        string pokemonName = PKMAPIUtilities.GetPkmName();

        //Assert
        Assert.Equal("pikachu", pokemonName);
    }

    [Fact]
    public void GetPkmStatsReturnsCorrectPokemonStats()
    {
        //Arrange
        JsonNode pokemonJson = JsonNode.Parse(helperFile.PikachuJson)!;
        var pikachuFromJson = PKMAPIUtilities.PokemonFromJson(pokemonJson);    

        //Act
        var pokemonStats = PKMAPIUtilities.GetPkmStats();

        //Assert
        Assert.NotNull(pokemonStats);
        Assert.IsType<List<PokemonBaseStat>>(pokemonStats);
        Assert.Equal(6, pokemonStats.Count);
        Assert.All(pokemonStats, stat => Assert.IsType<PokemonBaseStat>(stat));
    }

    [Fact]
    public void GetPkmSpritesReturnsCorrectPokemonSprites()
    {
        //Arrange
        JsonNode pokemonJson = JsonNode.Parse(helperFile.PikachuJson)!;
        var pikachuFromJson = PKMAPIUtilities.PokemonFromJson(pokemonJson);
        string[] pikachuSprites = helperFile.pikachuSprites;    

        //Act
        var pokemonSprites = PKMAPIUtilities.GetPkmSprites();

        //Assert
        Assert.NotNull(pokemonSprites);
        Assert.Equal(pikachuSprites[0], pokemonSprites.FrontDefault);
        Assert.Equal(pikachuSprites[1], pokemonSprites.FrontShiny);
        Assert.Equal(pikachuSprites[2], pokemonSprites.FrontFemale);
        Assert.Equal(pikachuSprites[3], pokemonSprites.FrontShinyFemale);
    }

    [Fact]
    public void GetPkmAbilitiesReturnsCorrectPokemonAbilities()
    {
        //Arrange
        JsonNode pokemonJson = JsonNode.Parse(helperFile.PikachuJson)!;
        var pikachuFromJson = PKMAPIUtilities.PokemonFromJson(pokemonJson);    

        //Act
        var pokemonAbilities = PKMAPIUtilities.GetPkmAbilities();

        //Assert
        Assert.NotNull(pokemonAbilities);
        Assert.IsType<List<PokemonAbility>>(pokemonAbilities);
        Assert.Equal(2, pokemonAbilities.Count);
        Assert.All(pokemonAbilities, ability => Assert.IsType<PokemonAbility>(ability));
    }

    [Fact]
    public void GetPkmMovesReturnsCorrectPokemonMoves()
    {
        //Arrange
        JsonNode pokemonJson = JsonNode.Parse(helperFile.PikachuJson)!;
        var pikachuFromJson = PKMAPIUtilities.PokemonFromJson(pokemonJson);    

        //Act
        var pokemonMoves = PKMAPIUtilities.GetPkmMoves();

        //Assert
        Assert.NotNull(pokemonMoves);
        Assert.IsType<List<PokemonMove>>(pokemonMoves);
        Assert.Equal(105, pokemonMoves.Count);
        Assert.All(pokemonMoves, ability => Assert.IsType<PokemonMove>(ability));
    }

    [Fact]
    public void GetPkmTypesReturnsCorrectPokemonTypes()
    {
        //Arrange
        JsonNode pokemonJson = JsonNode.Parse(helperFile.PikachuJson)!;
        var pikachuFromJson = PKMAPIUtilities.PokemonFromJson(pokemonJson);    

        //Act
        var pokemonTypes = PKMAPIUtilities.GetPkmTypes();

        //Assert
        Assert.NotNull(pokemonTypes);
        Assert.IsType<List<PokemonType>>(pokemonTypes);
        Assert.Single(pokemonTypes);
        Assert.All(pokemonTypes, ability => Assert.IsType<PokemonType>(ability));
    }
}