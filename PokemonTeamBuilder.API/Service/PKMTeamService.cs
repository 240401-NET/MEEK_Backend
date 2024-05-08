using Microsoft.IdentityModel.Tokens;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.Exceptoins;
using PokemonTeamBuilder.API.DTO;
using pkmtUtil = PokemonTeamBuilder.API.Utilities.PKMTeamUtilities;
using System.Collections.ObjectModel;

namespace PokemonTeamBuilder.API.Service;

public class PKMTeamServices : IPKMTeamService
{
    private readonly IPKMTeamRepo _pkmTeamRepo;
    private readonly IPKMAPIService _pkmAPIService;
    private readonly IPTMService _ptmService;
    private readonly string _pkmAPIBaseUrl = "https://pokeapi.co/api/v2/pokemon/";
    public PKMTeamServices(IPKMTeamRepo pkmTeamRepo, IPKMAPIService pkmAPIService, IPTMService pkmTMService){
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
            foreach(TeamMemberDTO teamMember in pkmTeam.PokemonTeamMembers)
            {
                PokemonTeamMember newMember = pkmtUtil.PkmTMFromDTO(teamMember);
                int pokeAPIId = newMember.PkmApiId;
                
                var officialPoke = _pkmAPIService.GetPkmFromDB(pokeAPIId);
                officialPoke ??= _pkmAPIService.GetPokemonFromAPI(_pkmAPIBaseUrl + pokeAPIId).Result;

                if(officialPoke is null) continue;
                
                newMember.Name = officialPoke.Name;
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

    public PokemonTeam UpdateTeam(PokemonTeamDTO pkmTeamDTO, int trainerId)
    {
        int teamId = pkmTeamDTO.Id ?? -1;

        if(teamId == -1)
        {
            return null!;
        }

        List<int> allTeamId = _pkmTeamRepo.GetAllTeamId(trainerId);

        if(!allTeamId.Contains(teamId))
        {
            return null!;
        }

        PokemonTeam oldTeam = _pkmTeamRepo.GetTeam(teamId).Result!;
        PokemonTeam newTeam = new();
        newTeam.Id = oldTeam.Id;
        newTeam.Name = pkmTeamDTO.Name;
        
        foreach(TeamMemberDTO pkmMember in pkmTeamDTO.PokemonTeamMembers)
        {
            PokemonTeamMember newMember = pkmtUtil.PkmTMFromDTO(pkmMember);
            newMember.PokemonTeamId = teamId;
            newTeam.PokemonTeamMembers.Add(newMember);
        }

        newTeam.PokemonTeamMembers = newTeam.PokemonTeamMembers.Take(6).ToList();

        return _pkmTeamRepo.UpdateTeam(newTeam);
    }

    public PokemonTeam DeleteTeam(int trainerId, int teamId){
        List<int> allTeamId = _pkmTeamRepo.GetAllTeamId(trainerId);

        if(!allTeamId.Contains(teamId))
        {
            return null!;
        }

        return _pkmTeamRepo.DeleteTeam(teamId);
    }      

    private Task<bool> DoesTeamExist(string name) => _pkmTeamRepo.DoesTeamExist(name);
    private Task<bool> DoesTeamExist(int id) => _pkmTeamRepo.DoesTeamExist(id);
}
