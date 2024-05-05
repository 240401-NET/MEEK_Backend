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
    
    [HttpGet("/TestMe")]
    public IActionResult TestMe()
    {
        PokemonTeam pkmTeam = new();
        
        if(User.Identity!.IsAuthenticated)
        {
            string username = User.Identity.Name!;
            var user = _userService.GetUserByName(username);
            pkmTeam.TrainerId = (int)user!.TrainerId!;
        }
        
        return Ok(pkmTeam);
    }

    [HttpGet("/TestMe2")]
    public IActionResult TestMe2()
    {
        PokemonTeamDTO pkmTeam = new();  
        // pkmTeam.PokemonStats.Add(new PokemonStat());     
        // pkmTeam.PokemonStats.Add(new PokemonStat());     
        return Ok(pkmTeam);
    }

    [HttpPost("/PostPkmTM")]
    public IActionResult TestMe3(PokemonTeamMember pkmTM, [FromQuery]int teamId)
    {
        var pkmTeam = _ptmService.AddPkmToTeam(pkmTM, teamId);        
        return Ok(pkmTeam);
    }

    //---------------------------------------TESTING ABOVE------------------             
    
    [Authorize]
    [HttpGet("/Teams")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetAllTeams(){
        int trainerID = _trainerService.GetTrainerIdByUsername(User!.Identity!.Name!);

        var teamList = _pkmTeamService.GetAll(trainerID);

        if(!teamList.Any())
        {
            return NoContent();
        }
        
        return Ok(teamList);        
    }
    
    [Authorize]
    [HttpPost("/Team")]
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

    [HttpPut("/Team/{pkmTeam}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> UpdateTeam(PokemonTeam pkmTeam){
        try{
            return Ok(_pkmTeamService.UpdateTeam(pkmTeam));
        }catch(NullReferenceException e){
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
        }catch(NullReferenceException e){
            return Conflict(e.Message);
        }
    }

    [HttpGet("/Team/name={name}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<PokemonTeam>> GetTeam(string name){
        try{
            return Ok(_pkmTeamService.GetTeam(name));
        }catch(NullReferenceException e){
            return Conflict(e.Message);
        }
    }

   
}