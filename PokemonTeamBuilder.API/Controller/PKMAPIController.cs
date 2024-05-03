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
    private readonly IPKMAPISevice _pkmAPISevice;

    public PKMAPIController(IPKMAPISevice pkmAPISevice) => _pkmAPISevice = pkmAPISevice;
    
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
    public IActionResult TestMe(string name)
    {
        var pkm = _pkmAPISevice.GetPokemonByName(name.ToLower()).Result;
        
        if(pkm is null)
        {
            return NotFound();
        }

        return Ok(pkm);
    }
    
}