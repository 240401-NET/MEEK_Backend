using Microsoft.AspNetCore.Mvc;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Exceptoins;

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
        }catch(EmptyListException _e){
            return NoContent();
        }
    }
    
}