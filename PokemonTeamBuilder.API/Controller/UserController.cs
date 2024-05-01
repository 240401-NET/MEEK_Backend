using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;

namespace PokemonTeamBuilder.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{    
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registration)
    {
        var result = await _userService.RegisterUser(registration);

        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUser login, [FromQuery] bool? useCookies, [FromQuery] bool? useSessionCookies)
    {        
        var result = await _userService.LoginUser(login, useCookies, useSessionCookies);

        if(!result.Succeeded)
        {
            return Unauthorized();
        }

        return Ok();
    }

    [HttpPost("/logout")]
    public IActionResult Logout()
    {
        _userService.Logout();
        return NoContent();
    }
}