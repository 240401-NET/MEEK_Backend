using System.Text.Json;
using System.Text.Json.Nodes;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using pkmAPIUtil = PokemonTeamBuilder.API.Model.Utilities.PKMAPIUtilities;

namespace PokemonTeamBuilder.API.Service;

public class PKMAPISevice : IPKMAPISevice
{
    private readonly IPKMAPIRepository _pkmAPIRepository;
    private readonly HttpClient _httpClient;    
    private readonly string _pkmAPIBaseUrl = "https://pokeapi.co/api/v2";

    public PKMAPISevice(HttpClient httpClient, IPKMAPIRepository pkmAPIRepository)
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

    public static async Task<ItemPokeApi> HTTPToItem(HttpResponseMessage httpResponse){
        if(httpResponse is not null && httpResponse.IsSuccessStatusCode)
        {
            var responseBody = await httpResponse.Content.ReadAsStringAsync();
            JsonNode pokemonJSON = JsonNode.Parse(responseBody)!;
            ItemPokeApi pkmAPIRes = pkmAPIUtil.ItemFromJson(pokemonJSON);
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

        if(httpResponse is not null && httpResponse.Result.IsSuccessStatusCode)
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
        PokemonPokeApi? pokemon = _pkmAPIRepository.GetPkmByIdFromDB(pokemonId);

        if(pokemon is not null)
        {
            return pokemon;
        }

        string pathParam = "/pokemon/" + pokemonId;
        string endpoint = _pkmAPIBaseUrl + pathParam;

        var httpResponse = CallPKMAPI(endpoint);

        if(httpResponse is not null && httpResponse.Result.IsSuccessStatusCode)
        {
            pokemon = await HttpToPKM(httpResponse.Result);
            _pkmAPIRepository.CreateNewPkmOnDB(pokemon);
        }

        return pokemon!;               
    }

    public async Task<PokemonPokeApi> GetPokemonByName(string pokemonName)
    {
        PokemonPokeApi? pokemon = _pkmAPIRepository.GetPkmByNameFromDB(pokemonName);

        if(pokemon is not null)
        {
            return pokemon;
        }
        
        string pathParam = "/pokemon/" + pokemonName;
        string endpoint = _pkmAPIBaseUrl + pathParam;

        var httpResponse = CallPKMAPI(endpoint);

        if(httpResponse is not null && httpResponse.Result.IsSuccessStatusCode)
        {
            pokemon = await HttpToPKM(httpResponse.Result);
            _pkmAPIRepository.CreateNewPkmOnDB(pokemon);
        }

        return pokemon!;
    }

    public async Task<ItemPokeApi> GetItemById(int itemID){
        ItemPokeApi? item = _pkmAPIRepository.GetItemByIDFromDB(itemID);
        if (item is not null)
            return item;

        string pathParam = "/item/" + itemID;
        string endpoint = _pkmAPIBaseUrl + pathParam;

        var httpResponse = CallPKMAPI(endpoint);

        if(httpResponse is not null && httpResponse.Result.IsSuccessStatusCode)
        {
            item = await HTTPToItem(httpResponse.Result);
            _pkmAPIRepository.CreateNewItemOnDB(item);
        }

        return item!; 
    }
}