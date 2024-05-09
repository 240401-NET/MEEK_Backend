using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Repository;
using Moq;

namespace PokemonTeamBuilder.Tests;

public class TrainerServiceTest
{
    Mock<ITrainerRepository> _trainerRepoMock;
    Mock<IUserRepository> _userRepoMock;
    TrainerService _service;
    Trainer _mockTrainer;
    Trainer _mockTrainerNoID;

    public TrainerServiceTest(){
        _trainerRepoMock = new();
        _userRepoMock = new();
        _mockTrainer = new(){
            Id = 1,
            Name = "mockTrainer"
        };
        _mockTrainerNoID = new(){
            Name = "mockTrainer"
        };
        _service = new(_trainerRepoMock.Object, _userRepoMock.Object);
    }

    [Fact]
    public void CreateTrainerTest()
    {
        _trainerRepoMock.Setup(repository => repository.CreateTrainer(_mockTrainerNoID.Name)).Returns(_mockTrainer);
        Assert.Equal(_mockTrainer.Id, _service.CreateTrainer(_mockTrainerNoID.Name).Id);
        _trainerRepoMock.Verify(repository => repository.CreateTrainer(_mockTrainerNoID.Name), Times.Exactly(1));        
    }

    [Fact]
    public void GetTrainerIdByUsernameTest()
    {
        _userRepoMock.Setup(repository => repository.GetTrainerId(_mockTrainerNoID.Name)).Returns(_mockTrainer.Id);
        Assert.Equal(_mockTrainer.Id, _service.GetTrainerIdByUsername(_mockTrainerNoID.Name));
        _userRepoMock.Verify(repository => repository.GetTrainerId(_mockTrainerNoID.Name), Times.Exactly(1));        
    }
}