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
    private readonly ITrainerService _trainerService;

    public TrainerController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpGet("/Trainer"), Authorize]
    public IActionResult GetTrainer()
    {
        return Ok(HttpContext.User.Identity?.Name);
    }

    [HttpPost("/Trainer"), Authorize]
    public IActionResult CreateTrainer(string name)
    {
        return Ok(_trainerService.CreateTrainer(name));        
        //return Ok(HttpContext.User.Identity?.Name);
    }
}