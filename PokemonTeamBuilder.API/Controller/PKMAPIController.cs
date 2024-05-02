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
    
    [HttpGet("/pokemon/id/")]
    public IActionResult TestMe()
    {
        _pkmAPISevice.TestMe();
        return Ok();
    }
    
}