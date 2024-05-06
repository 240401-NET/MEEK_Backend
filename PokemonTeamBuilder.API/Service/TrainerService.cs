using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;

namespace PokemonTeamBuilder.API.Service;

public class TrainerService : ITrainerService
{
    private readonly ITrainerRepository _trainerRepository;
    private readonly IUserRepository _userRepository;

    public TrainerService(ITrainerRepository trainerRepository, IUserRepository userRepository)
    {
        _trainerRepository = trainerRepository;
        _userRepository = userRepository;
    }
    
    public Trainer CreateTrainer(string name)
    {
        return _trainerRepository.CreateTrainer(name);
    }

    public int GetTrainerIdByUsername(string username)
    {
        return _userRepository.GetTrainerId(username);
    }
}