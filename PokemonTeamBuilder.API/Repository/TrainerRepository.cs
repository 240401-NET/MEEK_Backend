using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;

namespace PokemonTeamBuilder.API.Repository;

public class TrainerRepository : ITrainerRepository
{
    private readonly PokemonTrainerDbContext _trainerContext;

    public TrainerRepository(PokemonTrainerDbContext trainerContext) => _trainerContext = trainerContext;

    public Trainer CreateTrainer(string name)
    {
       Trainer newTrainer = new(){Name = name};
        _trainerContext.Trainers.Add(newTrainer);
        _trainerContext.SaveChanges();

        return newTrainer;
    }

    public Trainer? GetTrainerById(int id)
    {
        var trainer = _trainerContext.Trainers.Find(id);
        return trainer;
    }

    public Trainer? GetTrainerByName(string name)
    {
        var trainer = _trainerContext.Trainers.FirstOrDefault(x => x.Name.Equals(name));
        return trainer;
    }

    public void DeleteTrainer(Trainer trainer)
    {
        _trainerContext.Trainers.Remove(trainer);
        _trainerContext.SaveChanges();
    }
}