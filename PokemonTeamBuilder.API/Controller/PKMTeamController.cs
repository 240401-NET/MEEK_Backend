using Microsoft.AspNetCore.Mvc;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Exceptoins;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using Microsoft.AspNetCore.Authentication.Cookies;
using PokemonTeamBuilder.API.DTO;

namespace PokemonTeamBuilder.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class PKMTeamController : ControllerBase{
    private readonly IPKMTeamService _pkmTeamService;
    private readonly IPTMService _ptmService;
    private readonly IUserService _userService;
    private readonly ITrainerService _trainerService;
    
    public PKMTeamController(IPKMTeamService pkmTeamService, IUserService userService, IPTMService ptmService, ITrainerService trainerService)
    {
        _pkmTeamService = pkmTeamService;
        _userService = userService;
        _ptmService = ptmService;
        _trainerService = trainerService;
    }           
    
    [HttpGet("/Team"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult GetAllTeams(){
        int trainerID = _trainerService.GetTrainerIdByUsername(User!.Identity!.Name!);

        var teamList = _pkmTeamService.GetAll(trainerID);

        if(!teamList.Any())
        {
            return NoContent();
        }
        
        return Ok(teamList);        
    }
    
    [HttpPost("/Team"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult CreateNewTeam([FromBody] PokemonTeamDTO pkmTeam){
        int trainerID = _trainerService.GetTrainerIdByUsername(User!.Identity!.Name!);
        var newTeam = _pkmTeamService.CreateNewTeam(pkmTeam, trainerID);

        if(newTeam is null)
        {
            return BadRequest();
        }
        
        return Ok(newTeam);        
    }

    [HttpPut("/Team"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult UpdateTeam([FromBody] PokemonTeamDTO pkmTeam)
    {
        int trainerID = _trainerService.GetTrainerIdByUsername(User!.Identity!.Name!);
        var updatedTeam = _pkmTeamService.UpdateTeam(pkmTeam, trainerID);

        if(updatedTeam is null)
        {
            return BadRequest();
        }

        return Ok(updatedTeam);
    }

    // Check for login user here!
    [HttpDelete("/Team"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult DeleteTeam([FromBody] DeletePokemonTeamDTO deleteTeam)
    {
        int trainerID = _trainerService.GetTrainerIdByUsername(User!.Identity!.Name!);
        int teamIdToDelete = deleteTeam.Id;
        
        var teamDeleted = _pkmTeamService.DeleteTeam(trainerID, teamIdToDelete);
        
        if(teamDeleted is null)
        {
            return BadRequest();
        }

        return Ok(teamDeleted);
    }

    [HttpGet("/Team/id={id}"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetTeam(int id){
        try{
            return Ok(_pkmTeamService.GetTeam(id));
        }catch(NullReferenceException e){
            return Conflict(e.Message);
        }
    }

    [HttpGet("/Team/name={name}"), Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetTeam(string name){
        try{
            return Ok(_pkmTeamService.GetTeam(name));
        }catch(NullReferenceException e){
            return Conflict(e.Message);
        }
    }   
}