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
        _trainerContext.Trainers.Add(new Trainer(){Name = name});
        _trainerContext.SaveChanges();

        return GetTrainerByName(name)!;
    }

    public Trainer? GetTrainerById(int id)
    {
        var trainer = _trainerContext.Trainers.Find(id);
        return trainer;
    }

    public Trainer? GetTrainerByName(string name)
    {
        var trainer = _trainerContext.Trainers.Where(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();

        return trainer;
    }

    public void DeleteTrainer(Trainer trainer)
    {
        _trainerContext.Trainers.Remove(trainer);
        _trainerContext.SaveChanges();
    }
}