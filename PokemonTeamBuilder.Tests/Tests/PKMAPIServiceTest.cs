using System.Collections.ObjectModel;
using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using Microsoft.AspNetCore.Http;
using Moq;
using Moq.Protected;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.Service;
using helperFile = PokemonTeamBuilder.Tests.Helpers.TestHelpers;

namespace PokemonTeamBuilder.Tests;

public class PKMAPIServiceTest
{
    [Fact]
    public async void GetAllPokemonReturnsNotNullandIEnumerableOfPokemonNameandURL()
    {
        //Arrange
        Mock<IPKMAPIRepository> repoMock = new();
        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemonList = await service.GetAllPokemon();

        //Assert
        Assert.NotNull(pokemonList);
        Assert.IsType<PokemonNameandURL[]>(pokemonList);
    }
    
    [Fact]
    public async void GetAllPokemonIsEmptyWhenHTTPResponseIsBad()
    {
        //Arrange
        Mock<IPKMAPIRepository> repoMock = new();
        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemonList = await service.GetAllPokemon();

        //Assert
        Assert.Empty(pokemonList);
    }

    [Fact]
    public async void GetAllPokemonWhenHTTPResponseIsGood()
    {
        //Arrange
        string pokemonString = helperFile.PokemonListJson;
        Mock<IPKMAPIRepository> repoMock = new();
        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.OK, Content = new StringContent(pokemonString)});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemonList = await service.GetAllPokemon();

        //Assert
        Assert.NotEmpty(pokemonList);
        Assert.Equal(3, pokemonList.Count());
    }

    [Fact]
    public async void GetPokemonByIdIsNullWhenDBEmptyAndAPIPResponseIsBad()
    {
        //Arrange
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(It.IsAny<int>())).Returns<IEnumerable<int>>(null!);

        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = await service.GetPokemonById(1);

        //Assert
        Assert.Null(pokemon);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<int>()), Times.Exactly(1));
        repoMock.Verify(repo => repo.CreateNewPkmOnDB(It.IsAny<PokemonPokeApi>()), Times.Never());
    }

    [Fact]
    public async void GetPokemonByIdReturnsPokemonWhenDBNotEmpty()
    {
        //Arrange
        PokemonPokeApi pikachu = new()
        {
            Id = 25,
            Name = "pikachu"
        };
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(25)).Returns(pikachu);

        Mock<HttpClient> httpClientMock = new();

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = await service.GetPokemonById(25);

        //Assert
        Assert.NotNull(pokemon);
        Assert.Equal("pikachu", pokemon.Name);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<int>()), Times.Exactly(1));
        repoMock.Verify(repo => repo.CreateNewPkmOnDB(It.IsAny<PokemonPokeApi>()), Times.Never);
    }

    [Fact]
    public async void GetPokemonByIdReturnsPokemonWhenDBEmptyAndAPIResponseIsGood()
    {
        //Arrange
        string pikachu = helperFile.PikachuJson;
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(It.IsAny<int>())).Returns<IEnumerable<int>>(null!);

        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.OK, Content = new StringContent(pikachu)});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = await service.GetPokemonById(1);

        //Assert
        Assert.NotNull(pokemon);
        Assert.Equal("pikachu", pokemon.Name);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<int>()), Times.Exactly(1));
        repoMock.Verify(repo => repo.CreateNewPkmOnDB(It.IsAny<PokemonPokeApi>()), Times.Exactly(1));
    }

    [Fact]
    public async void GetPokemonByNameIsNullWhenDBEmptyAndAPIPResponseIsBad()
    {
        //Arrange
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(It.IsAny<string>())).Returns<IEnumerable<int>>(null!);

        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = await service.GetPokemonByName("pikachu");

        //Assert
        Assert.Null(pokemon);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<string>()), Times.Exactly(1));
        repoMock.Verify(repo => repo.CreateNewPkmOnDB(It.IsAny<PokemonPokeApi>()), Times.Never());
    }

    [Fact]
    public async void GetPokemonByNameReturnsPokemonWhenDBNotEmpty()
    {
        //Arrange
        PokemonPokeApi pikachu = new()
        {
            Id = 25,
            Name = "pikachu"
        };
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB("pikachu")).Returns(pikachu);

        Mock<HttpClient> httpClientMock = new();

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = await service.GetPokemonByName("pikachu");

        //Assert
        Assert.NotNull(pokemon);
        Assert.Equal("pikachu", pokemon.Name);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<string>()), Times.Exactly(1));
        repoMock.Verify(repo => repo.CreateNewPkmOnDB(It.IsAny<PokemonPokeApi>()), Times.Never);
    }

    [Fact]
    public async void GetPokemonByNameReturnsPokemonWhenDBEmptyAndAPIResponseIsGood()
    {
        //Arrange
        string pikachu = helperFile.PikachuJson;
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(It.IsAny<string>())).Returns<IEnumerable<int>>(null!);

        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.OK, Content = new StringContent(pikachu)});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = await service.GetPokemonByName("pikachu");

        //Assert
        Assert.NotNull(pokemon);
        Assert.Equal("pikachu", pokemon.Name);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<string>()), Times.Exactly(1));
        repoMock.Verify(repo => repo.CreateNewPkmOnDB(It.IsAny<PokemonPokeApi>()), Times.Exactly(1));
    }

    [Fact]
    public async void GetPokemonFromAPIReturnsNullWhenAPIResponseIsBad()
    {
        //Arrange
        string endpoint  = "https://pokeapi.co/api/v2/pokemon/1";
        Mock<IPKMAPIRepository> repoMock = new();
        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.BadRequest});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = await service.GetPokemonFromAPI(endpoint);

        //Assert
        Assert.Null(pokemon);
        repoMock.Verify(repo => repo.CreateNewPkmOnDB(It.IsAny<PokemonPokeApi>()), Times.Never);
    }

    [Fact]
    public async void GetPokemonFromAPIReturnsPokemonWhenAPIResponseIsGood()
    {
        //Arrange
        string pikachu  = helperFile.PikachuJson;
        string endpoint  = "https://pokeapi.co/api/v2/pokemon/1";
        Mock<IPKMAPIRepository> repoMock = new();
        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        
        httpMsgHandlerMock.Protected()
            .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage {StatusCode = HttpStatusCode.OK, Content = new StringContent(pikachu)});

        Mock<HttpClient> httpClientMock = new(httpMsgHandlerMock.Object);

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = await service.GetPokemonFromAPI(endpoint);

        //Assert
        Assert.NotNull(pokemon);
        Assert.Equal("pikachu", pokemon.Name);
        repoMock.Verify(repo => repo.CreateNewPkmOnDB(It.IsAny<PokemonPokeApi>()), Times.Exactly(1));
    }

    [Fact]
    public void GetPkmFromDB_id_ReturnsNullWhenPokemonDoesNotExist()
    {
        //Arrange
        int pokemonId = 4;
        List<PokemonPokeApi> pokemonList = helperFile.PokemonList;
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(pokemonId)).Returns(pokemonList.FirstOrDefault(pkm => pkm.Id == pokemonId));

        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        Mock<HttpClient> httpClientMock = new();

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = service.GetPkmFromDB(pokemonId);

        //Assert
        Assert.Null(pokemon);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<int>()), Times.Exactly(1));
    }

    [Theory]
    [InlineData(1, "bulbasaur")]
    [InlineData(2, "ivysaur")]
    [InlineData(3, "venusaur")]
    public void GetPkmFromDB_id_ReturnsCorrectPokemonFromDB(int id, string name)
    {
        //Arrange
        List<PokemonPokeApi> pokemonList = helperFile.PokemonList;
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(id)).Returns(pokemonList.FirstOrDefault(pkm => pkm.Id == id));

        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        Mock<HttpClient> httpClientMock = new();

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = service.GetPkmFromDB(id);

        //Assert
        Assert.NotNull(pokemon);
        Assert.Equal(name, pokemon.Name);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<int>()), Times.Exactly(1));
    }

    [Fact]
    public void GetPkmFromDB_name_ReturnsNullWhenPokemonDoesNotExist()
    {
        //Arrange
        string pokemonName = "slowbro";
        List<PokemonPokeApi> pokemonList = helperFile.PokemonList;
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(pokemonName)).Returns(pokemonList.FirstOrDefault(pkm => pkm.Name.Equals(pokemonName)));

        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        Mock<HttpClient> httpClientMock = new();

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = service.GetPkmFromDB(pokemonName);

        //Assert
        Assert.Null(pokemon);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<string>()), Times.Exactly(1));
    }

    [Theory]
    [InlineData(1, "bulbasaur")]
    [InlineData(2, "ivysaur")]
    [InlineData(3, "venusaur")]
    public void GetPkmFromDB_name_ReturnsCorrectPokemonFromDB(int id, string name)
    {
        //Arrange
        List<PokemonPokeApi> pokemonList = helperFile.PokemonList;
        Mock<IPKMAPIRepository> repoMock = new();

        repoMock.Setup(repo => repo.GetPkmFromDB(name)).Returns(pokemonList.FirstOrDefault(pkm => pkm.Name.Equals(name)));

        Mock<HttpMessageHandler> httpMsgHandlerMock = new();        
        Mock<HttpClient> httpClientMock = new();

        PKMAPIService service = new(httpClientMock.Object, repoMock.Object);

        //Act
        var pokemon = service.GetPkmFromDB(name);

        //Assert
        Assert.NotNull(pokemon);
        Assert.Equal(id, pokemon.Id);
        repoMock.Verify(repo => repo.GetPkmFromDB(It.IsAny<string>()), Times.Exactly(1));
    }
}