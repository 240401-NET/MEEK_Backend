using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Repository;

public class PTMRepository : IPTMRepository
{
    private readonly PokemonTrainerDbContext _context;

    public PTMRepository(PokemonTrainerDbContext context) => _context = context;

    public IEnumerable<PokemonTeamMember> GetAllPTMByTeamId(int teamId)
    {
        return _context.PokemonTeamMembers
        .Include(pkm => pkm.PokemonStats)
        .Where(pkm => pkm.PokemonTeamId == teamId);
    }    
    
    public PokemonTeamMember GetPTMById(int id)
    {
        return _context.PokemonTeamMembers
        .Include(pkm => pkm.PokemonStats)
        .FirstOrDefault(pkm => pkm.Id == id)!;
    }    

    public PokemonTeam GetPkmTeamById(int id)
    {
        return _context.PokemonTeams
        .Include(pkm => pkm.PokemonTeamMembers)
        .FirstOrDefault(pkm => pkm.Id == id)!;
    }
    
    
    public PokemonTeamMember UpdatePTM(PokemonTeamMember updatedPKM)
    {
        PokemonTeamMember oldPKM = GetPTMById(updatedPKM.Id);
        
        _context.PokemonTeamMembers.Entry(oldPKM).CurrentValues.SetValues(updatedPKM);
        _context.SaveChanges();
        
        return updatedPKM;
    }

    public PokemonTeam AddPkmToTeam(PokemonTeamMember newPKM, int teamId)
    {
        PokemonTeam pkmTeam = GetPkmTeamById(teamId);
        pkmTeam.PokemonTeamMembers.Add(newPKM);
        _context.SaveChanges(); 
        return pkmTeam;
    }
}