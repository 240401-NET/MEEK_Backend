using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Repository;
public class PKMTeamRepository : IPKMTeamRepo
{
    public readonly PokemonTrainerDbContext _context;

    public PKMTeamRepository(PokemonTrainerDbContext context) => _context = context;

    public async Task<IEnumerable<PokemonTeam?>> GetAll(int trainerID){     
       return await _context.PokemonTeams.Include(c => c.PokemonTeamMembers).Where(p => p.TrainerId == trainerID).ToListAsync();
    } 

    public async Task<PokemonTeam> GetTeam(int id) {
        return await _context.PokemonTeams.Where(p => p.Id == id).SingleAsync();
    }
    public async Task<PokemonTeam> GetTeam(string name){
        return await _context.PokemonTeams.Where(p => p.Name == name).SingleAsync();
    }
    public async Task<PokemonTeam> CreateNewTeam(PokemonTeam team){
        _context.PokemonTeams.Add(team);
        await _context.SaveChangesAsync();
        return team;
    }
    public async Task<PokemonTeam> UpdateTeam(PokemonTeam pkmTeam){
        PokemonTeam oldTeam = GetTeam(pkmTeam.Id).Result;
        _context.PokemonTeams.Entry(oldTeam).CurrentValues.SetValues(pkmTeam);
        await _context.SaveChangesAsync();
        return pkmTeam;
    }
    public async Task<PokemonTeam> DeleteTeam(int id){
        PokemonTeam? tempPKM = GetTeam(id).Result;
        _context.PokemonTeams.Remove(tempPKM);
        await _context.SaveChangesAsync();
        tempPKM.Id = -1;
        return tempPKM;
    }

    public async Task<bool> DoesTeamExist(string name){
        return (await _context.PokemonTeams.CountAsync(p => p.Name == name)) > 0 ? true : false;
    }
    public async Task<bool> DoesTeamExist(int id){
        return (await _context.PokemonTeams.CountAsync(p => p.Id == id)) > 0 ? true : false;
    }
}