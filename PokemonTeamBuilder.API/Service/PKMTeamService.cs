using Microsoft.IdentityModel.Tokens;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.Exceptoins;
using PokemonTeamBuilder.API.DTO;
using pkmtUtil = PokemonTeamBuilder.API.Utilities.PKMTeamUtilities;

namespace PokemonTeamBuilder.API.Service;

public class PKMTeamServices : IPKMTeamService
{
    private readonly IPKMTeamRepo _pkmTeamRepo;
    private readonly IPKMAPISevice _pkmAPIService;
    private readonly IPTMService _ptmService;
    public PKMTeamServices(IPKMTeamRepo pkmTeamRepo, IPKMAPISevice pkmAPIService, IPTMService pkmTMService){
        _pkmTeamRepo = pkmTeamRepo; 
        _pkmAPIService = pkmAPIService;
        _ptmService = pkmTMService;
    }

    public PokemonTeam CreateNewTeam(PokemonTeamDTO pkmTeam, int trainerId){
        // All data should come in, but just incase we'll check cache
        PokemonTeam newTeam = _pkmTeamRepo.CreateNewTeam(new(){Name = pkmTeam.Name, TrainerId = trainerId});

        int teamId = newTeam.Id;

        if(newTeam is null)
        {
            return null!;
        }

        try
        {
            foreach(TeamMemberDTO teamMember in pkmTeam.TeamMembers)
            {
                PokemonTeamMember newMember = pkmtUtil.PkmTMFromDTO(teamMember);
                newMember.PokemonTeamId = teamId;
                _ptmService.AddPkmToTeam(newMember, teamId);
            }
        }catch(PkmTeamSizeException e)
        {
            Console.WriteLine(e.Message);
        }

        return newTeam;
    }

    public Task<PokemonTeam> GetTeam(int id){
        // get team members as well
        return DoesTeamExist(id).Result ? _pkmTeamRepo.GetTeam(id) : throw new NullReferenceException("The team does not exist in the database.");
    }

    public Task<PokemonTeam> GetTeam(string name){
        // get team members as well
        return DoesTeamExist(name).Result ? _pkmTeamRepo.GetTeam(name) : throw new NullReferenceException("The team does not exist in the database.");
    }

    public IEnumerable<PokemonTeam> GetAll(int trainerID)
    {
        // get team members as well
        var returnList = _pkmTeamRepo.GetAll(trainerID);
        return returnList;
    }

    public Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam)
    {
        // Get the old PokemonTeamMember list
        // check each pokemon (by roster order)
        // if the old pokemon in spot 'n' is different from the new pokemon in spot 'n'
        //      delete the old pokemon.
        // If they are the same, update the old pokemon with new pokemon information 
        // If there is no new pokemon in spot 'n' delete the old pokemon.

        return DoesTeamExist(pkmTeam.Id).Result ? _pkmTeamRepo.UpdateTeam(pkmTeam) : throw new NullReferenceException("The team does not exist in the database.");
    }

    public Task<PokemonTeam> DeleteTeam(int id){
        // delete the team member as well.
        return DoesTeamExist(id).Result ? _pkmTeamRepo.DeleteTeam(id) : throw new NullReferenceException("The team does not exist in the database.");
    }  


    

    

    private Task<bool> DoesTeamExist(string name) => _pkmTeamRepo.DoesTeamExist(name);
    private Task<bool> DoesTeamExist(int id) => _pkmTeamRepo.DoesTeamExist(id);
}
