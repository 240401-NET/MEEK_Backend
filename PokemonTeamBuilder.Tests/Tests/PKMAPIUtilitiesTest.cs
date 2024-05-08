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
}