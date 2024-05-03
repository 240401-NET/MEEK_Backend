using Microsoft.IdentityModel.Tokens;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.Exceptoins;

namespace PokemonTeamBuilder.API.Service;

public class PKMTeamServices : IPKMTeamService
{
    private readonly IPKMTeamRepo _pkmTeamRepo;
    private readonly IPKMAPISevice _pkmAPIService;
    private readonly IPTMService _pkmTMService;
    public PKMTeamServices(IPKMTeamRepo pkmTeamRepo, IPKMAPISevice pkmAPIService, IPTMService pkmTMService){
        _pkmTeamRepo = pkmTeamRepo; 
        _pkmAPIService = pkmAPIService;
        _pkmTMService = pkmTMService;
    }

    public async Task<PokemonTeam> CreateNewTeam(PokemonTeam pkmTeam){
        pkmTeam = DoesTeamExist(pkmTeam.Id).Result ? throw new ObjectExistException("This team is already in our database, please update the team.") : _pkmTeamRepo.CreateNewTeam(pkmTeam).Result;
        // All data should come in, but just incase we'll check cache
        foreach(PokemonTeamMember pkmTM in pkmTeam.PokemonTeamMembers){
            pkmTM.PkmApi = _pkmAPIService.GetPokemonById(pkmTM.PkmApiId).Result;
            pkmTM.HeldItem = _pkmAPIService.GetItemById(pkmTM.HeldItemId).Result;
            //_pkmTMService.AddPkmToTeam(pkmTM)

        }
        return pkmTeam;
    }

    public Task<PokemonTeam> DeleteTeam(int id){
        // delete the team member as well.
        return DoesTeamExist(id).Result ? _pkmTeamRepo.DeleteTeam(id) : throw new NullReferenceException("The team does not exist in the database.");
    }
    

    public Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID)
    {
        // get team members as well
        Task<IEnumerable<PokemonTeam?>> returnList = _pkmTeamRepo.GetAll(trainerID);
        return returnList.Result.ToList().IsNullOrEmpty() ? throw new EmptyListException("The list is empty.") : returnList;
    }

    public Task<PokemonTeam> GetTeam(int id){
        // get team members as well
        return DoesTeamExist(id).Result ? _pkmTeamRepo.GetTeam(id) : throw new NullReferenceException("The team does not exist in the database.");
    }

    public Task<PokemonTeam> GetTeam(string name){
        // get team members as well
        return DoesTeamExist(name).Result ? _pkmTeamRepo.GetTeam(name) : throw new NullReferenceException("The team does not exist in the database.");
    }

    public Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam){
        // Get the old PokemonTeamMember list
        // check each pokemon (by roster order)
        // if the old pokemon in spot 'n' is different from the new pokemon in spot 'n'
        //      delete the old pokemon.
        // If they are the same, update the old pokemon with new pokemon information 
        // If there is no new pokemon in spot 'n' delete the old pokemon.

        return DoesTeamExist(pkmTeam.Id).Result ? _pkmTeamRepo.UpdateTeam(pkmTeam) : throw new NullReferenceException("The team does not exist in the database.");
    }

    private Task<bool> DoesTeamExist(string name) => _pkmTeamRepo.DoesTeamExist(name);
    private Task<bool> DoesTeamExist(int id) => _pkmTeamRepo.DoesTeamExist(id);
}
