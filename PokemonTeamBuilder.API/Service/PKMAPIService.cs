using System.Text.Json;
using System.Text.Json.Nodes;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using pkmAPIUtil = PokemonTeamBuilder.API.Utilities.PKMAPIUtilities;

namespace PokemonTeamBuilder.API.Service;

public class PKMAPIService : IPKMAPIService
{
    private readonly IPKMAPIRepository _pkmAPIRepository;
    private readonly HttpClient _httpClient;    
    private readonly string _pkmAPIBaseUrl = "https://pokeapi.co/api/v2";

    public PKMAPIService(HttpClient httpClient, IPKMAPIRepository pkmAPIRepository)
    {
        _httpClient = httpClient;
        _pkmAPIRepository = pkmAPIRepository;
    }

    private async Task<HttpResponseMessage> CallPKMAPI(string endpoint)
    {
        return await _httpClient.GetAsync(endpoint);
    }

    private static async Task<PokemonPokeApi> HttpToPKM(HttpResponseMessage httpResponse)
    {
        if(httpResponse is not null && httpResponse.IsSuccessStatusCode)
        {
            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            JsonNode pokemonJSON = JsonNode.Parse(responseBody)!;
            PokemonPokeApi pkmAPIRes = pkmAPIUtil.PokemonFromJson(pokemonJSON);
            return pkmAPIRes;       
        }
        return null!;
    }

    public async Task<IEnumerable<PokemonNameandURL>> GetAllPokemon()
    {
        string pathParam = "/pokemon";
        string queryString = "?limit=1000000";
        string endpoint = _pkmAPIBaseUrl + pathParam + queryString;

        var httpResponse = CallPKMAPI(endpoint);

        if(httpResponse.Result.IsSuccessStatusCode)
        {
            var responseBody = await httpResponse.Result.Content.ReadAsStringAsync();

            JsonNode pokemonResults = JsonNode.Parse(responseBody)!["results"]!;

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            IEnumerable<PokemonNameandURL> pkmAPIRes = JsonSerializer.Deserialize<IEnumerable<PokemonNameandURL>>(pokemonResults, options ?? null)!;

            return pkmAPIRes;          
        }

        return [];
    }

    public async Task<PokemonPokeApi> GetPokemonById(int pokemonId)
    {
        string pathParam = "/pokemon/" + pokemonId;
        string endpoint = _pkmAPIBaseUrl + pathParam;        
        PokemonPokeApi pokemon = GetPkmFromDB(pokemonId) ?? await GetPokemonFromAPI(endpoint);
        return pokemon;               
    }

    public async Task<PokemonPokeApi> GetPokemonByName(string pokemonName)
    {
        string pathParam = "/pokemon/" + pokemonName;
        string endpoint = _pkmAPIBaseUrl + pathParam;        
        PokemonPokeApi pokemon = GetPkmFromDB(pokemonName) ?? await GetPokemonFromAPI(endpoint);
        return pokemon!;
    }

    public async Task<PokemonPokeApi> GetPokemonFromAPI(string endpoint)
    {
        var httpResponse = await CallPKMAPI(endpoint);
        
        if(httpResponse.IsSuccessStatusCode)
        {
            PokemonPokeApi pokemon = await HttpToPKM(httpResponse);
            _pkmAPIRepository.CreateNewPkmOnDB(pokemon);
            return pokemon;
        }
        return null!;
    }

    public PokemonPokeApi? GetPkmFromDB(int id)
    {
        return _pkmAPIRepository.GetPkmFromDB(id);
    }

    public PokemonPokeApi? GetPkmFromDB(string name)
    {
        return _pkmAPIRepository.GetPkmFromDB(name);
    }
}