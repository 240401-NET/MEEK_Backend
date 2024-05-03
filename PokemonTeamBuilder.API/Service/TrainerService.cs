using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;

namespace PokemonTeamBuilder.API.Service;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepository;

    public TrainerService(ITrainerRepository trainerRepository) => _trainerRepository = trainerRepository;
    
    public Trainer CreateTrainer(string name)
    {
        throw new NotImplementedException();
    }
}