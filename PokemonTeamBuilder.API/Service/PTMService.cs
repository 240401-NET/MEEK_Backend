using PokemonTeamBuilder.API.Exceptoins;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;

namespace PokemonTeamBuilder.API.Service;

public class PTMService : IPTMService
{
    private readonly IPTMRepository _PTMRepository;

    public PTMService(IPTMRepository PTMRepository)
    {
        _PTMRepository = PTMRepository;
    }

    public IEnumerable<PokemonTeamMember> GetAllPTMByTeamId(int teamId)
    {
        return _PTMRepository.GetAllPTMByTeamId(teamId);
    }

    public PokemonTeamMember UpdatePTM(PokemonTeamMember updatedPKM)
    {
        var oldPKM = _PTMRepository.GetPTMById(updatedPKM.Id);

        if(oldPKM is not null)
        {
            return _PTMRepository.UpdatePTM(updatedPKM);
        }

        return null!;
    }

    public PokemonTeam AddPkmToTeam(PokemonTeamMember newPKM, int teamId)
    {
        var pkmTeam = _PTMRepository.GetPkmTeamById(teamId);

        if(pkmTeam is not null)
        {
            if(pkmTeam.PokemonTeamMembers.Count == 6) 
                throw new PkmTeamSizeException("Team is already at max size (6)");
            
            return _PTMRepository.AddPkmToTeam(newPKM, teamId);
        }

        return null!;
    }

    public PokemonTeam DeletePTMFromTeam(PokemonTeamMember deletePKM)
    {
        throw new NotImplementedException();
    }

    public PokemonTeam DeleteAllPkmFromTeam(int teamId)
    {
        throw new NotImplementedException();
    }


}

// GetPTM(int teamID) --Retrieve list of pokemon apart of the team by team id and return it.
// UpdatePTM(PokemonTeamMember pkm) --Update pokemon team member and return newly updated pokemon.
// AddPTM(PokemonTeamMember pkm, int teamID) --Retrieve team by id and adds pokemon to the team. Return a team.
// DeletePTM(PokemonPokeAPI pokemon) --Delete the pokemon from the database and return it with id = -1.
// DeletePTM(int teamID) --Delete all pokemon from team and return and empty list.