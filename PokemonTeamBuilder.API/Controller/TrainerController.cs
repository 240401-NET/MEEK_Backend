using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;

namespace PokemonTeamBuilder.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class TrainerController : ControllerBase
{
    private readonly IUserService _userService;

    public TrainerController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("/Trainer"), Authorize]
    public IActionResult GetTrainer()
    {
        return Ok(HttpContext.User.Identity?.Name);
    }
}