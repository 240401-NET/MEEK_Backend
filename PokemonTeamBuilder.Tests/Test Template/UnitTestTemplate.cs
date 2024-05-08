using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.Tests;

public class UnitTestTemplate
{
    [Fact (Skip = "Test template, not real test")]
    public void Test()
    {
        //Arrange
        RegisterUser user = new()
        {
            Username = "TestUser",
            Email = "TestUser@email.com",
            Password = "PassWord@22"
        };

        //Act
        user.Username = "TestUser2";

        //Assert
        Assert.NotNull(user);
        Assert.Equal("TestUser2", user.Username);
    }
}