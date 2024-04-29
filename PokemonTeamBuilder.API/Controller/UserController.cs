using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{    
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;

    public UserController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("/register")]
    public async Task<IActionResult> RegisterUser([FromBody] RegisterUser registration)
    {
        // var usernameExist = await _userManager.FindByNameAsync(registration.Username);
        // var emailExist = await _userManager.FindByEmailAsync(registration.Email);
        
        // if(usernameExist is not null || emailExist is not null)
        // {
        //     return Conflict("Username or Email is already in use!");
        // }

        IdentityUser user = new()
        {
            UserName = registration.Username,
            Email = registration.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, registration.Password);


        if(!result.Succeeded)
        {
            return BadRequest(result.Errors);
        }

        return Ok();
    }

    [HttpPost("/login")]
    public async Task<IActionResult> LoginUser([FromBody] LoginUser login)
    {        
        _signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;

        var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, isPersistent: true, lockoutOnFailure: true);

        if(!result.Succeeded)
        {
            return Unauthorized(result);
        }

        return Ok();
    }

    [HttpPost("/logout")]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}