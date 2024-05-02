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
public class PKMTeamController : ControllerBase{
    private readonly IPKMTeamService _pkmTeamService;
    public PKMTeamController(IPKMTeamService pkmTeamService) => _pkmTeamService = pkmTeamService;
    
    
    // Check for login user here!
    [HttpGet("/Teams/{trainerID}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetAllTeams(int trainerID){
        try{
            return Ok(_pkmTeamService.GetAll(trainerID));
        }catch(EmptyListException){
            return NoContent();
        }
    }
    
    // Check for login user here!
    [HttpPost("/Team/{pkmTeam}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> CreateNewTeam(PokemonTeam pkmTeam){
        try{
            return Ok(_pkmTeamService.CreateNewTeam(pkmTeam));
        }catch(ObjectExistException e){
            return Conflict(e.Message);
        }
    }

    // Check for login user here!
    [HttpDelete("/Team/{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> DeleteTeam(int id){
        try{
            return Ok(_pkmTeamService.DeleteTeam(id));
        }catch(NullReferenceException e){
            return Conflict(e.Message);
        }
    }

    // Check for login user here!
    [HttpGet("/Team/id={id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetTeam(int id){
        try{
            return Ok(_pkmTeamService.GetTeam(id));
        }catch(EmptyListException){
            return NoContent();
        }
    }

    [HttpGet("/Team/name={name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetTeam(string name){
        try{
            return Ok(_pkmTeamService.GetTeam(name));
        }catch(EmptyListException){
            return NoContent();
        }
    }

    [HttpPut("/Team/{pkmTeam}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> UpdateTeam(PokemonTeam pkmTeam){
        try{
            return Ok(_pkmTeamService.UpdateTeam(pkmTeam));
        }catch(EmptyListException){
            return NoContent();
        }
    }
}