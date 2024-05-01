using System.Text.Json;
using System.Text.Json.Nodes;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Service;

public class PKMAPISevice : IPKMAPISevice
{
    private readonly HttpClient _httpClient;    
    private readonly string _pkmAPIBaseUrl = "https://pokeapi.co/api/v2";

    public PKMAPISevice(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<HttpResponseMessage> CallPKMAPI(string endpoint)
    {
        return await _httpClient.GetAsync(endpoint);
    }

    public async Task<IEnumerable<PokemonNameandURL>> GetAllPokemon()
    {
        string pathParam = "/pokemon";
        string queryString = "?limit=10";
        string endpoint = _pkmAPIBaseUrl + pathParam + queryString;

        var httpResponse = CallPKMAPI(endpoint);

        if(httpResponse is not null && httpResponse.Result.IsSuccessStatusCode)
        {
            var responseBody = await httpResponse.Result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            PokemonAPIResponse pkmAPIRes = JsonSerializer.Deserialize<PokemonAPIResponse>(responseBody, options ?? null)!;

            return pkmAPIRes.Pokemons;          
        }

        return [];
    }

    public async Task<PokemonPokeApi> GetPokemonById(int pokemonId)
    {
        string pathParam = "/pokemon/" + pokemonId;
        string endpoint = _pkmAPIBaseUrl + pathParam;

        var httpResponse = CallPKMAPI(endpoint);

        if(httpResponse is not null && httpResponse.Result.IsSuccessStatusCode)
        {
            var responseBody = await httpResponse.Result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            PokemonPokeApi pkmAPIRes = JsonSerializer.Deserialize<PokemonPokeApi>(responseBody, options ?? null)!;

            return pkmAPIRes;       
        }

        return null!;
    }

    public async void TestMe()
    {
        int pokemonId = 1;
        string pathParam = "/pokemon/" + pokemonId;
        string endpoint = _pkmAPIBaseUrl + pathParam;

        var httpResponse = CallPKMAPI(endpoint);

        if(httpResponse is not null && httpResponse.Result.IsSuccessStatusCode)
        {
            var responseBody = await httpResponse.Result.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            JsonNode pokemonJSON = JsonNode.Parse(responseBody)!;

            JsonNode testing = pokemonJSON["stats"]!;

            Console.WriteLine(testing);

            //PokemonPokeApi pkmAPIRes = JsonSerializer.Deserialize<PokemonPokeApi>(responseBody, options ?? null)!;
   
        }
    }
}