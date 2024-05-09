using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.Tests;

public class PokemonResponseTest
{
    [Fact]
    public void ToStringTest()
    {
        string name = "bulbasaur";
        string URL = "url";
        PokemonNameandURL testResponse = new PokemonNameandURL(){
            Name = name,
            URL = URL
       };
        Assert.Equal($"Name: {name}\n\tURL: {URL}", testResponse.ToString());
    }
}