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

        if(result.Succeeded)
        {
            _userService.CreateTrainerForUser(registration.Username);
            return Ok();
        }        
        return BadRequest(result.Errors);
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUser login)
    {        
        var result = await _userService.LoginUser(login);

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