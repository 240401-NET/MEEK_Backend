using Microsoft.AspNetCore.Mvc;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Exceptoins;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace PokemonTeamBuilder.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class PKMAPIController : ControllerBase{
    private readonly IPKMAPIService _pkmAPISevice;

    public PKMAPIController(IPKMAPIService pkmAPISevice) => _pkmAPISevice = pkmAPISevice;
    
    [HttpGet("/pokemon")]
    public IActionResult GetAllPokemon()
    {
        var allPokemon = _pkmAPISevice.GetAllPokemon();

        if(allPokemon is null)
        {
            return NotFound();
        }
        return Ok(allPokemon);
    }
    
    [HttpGet("/pokemon/id/{id}")]
    public IActionResult GetPkmById(int id)
    {
        var pkm = _pkmAPISevice.GetPokemonById(id).Result;

        if(pkm is null)
        {
            return NotFound();
        }

        return Ok(pkm);
    }

    [HttpGet("/pokemon/name/{name}")]
    public IActionResult GetPkmByName(string name)
    {
        var pkm = _pkmAPISevice.GetPokemonByName(name.ToLower()).Result;
        
        if(pkm is null)
        {
            return NotFound();
        }

        return Ok(pkm);
    }
    
}