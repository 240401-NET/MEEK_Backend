using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.DTO;
using Moq;
namespace PokemonTeamBuilder.Tests;

public class PTMServiceTest
{
    Mock<IPTMRepository> _ptmRepositoryMock;
    PokemonTeamMember _ptmMock;
    PTMService _service;
    PokemonTeam _mockTeam;
    PokemonTeam _mockTeamNoMembers;
    
    public PTMServiceTest(){
        _ptmRepositoryMock = new();
        _ptmMock = new PokemonTeamMember{
                Id = 1,
                PkmApiId = 1,
                Name = "bulbasaur",
                NickName = "Bulba",
                Level = 1,
                ChosenAbility = "Overgrow",
                Gender = true,
                IsShiny = false,
                TeraType = "Normal",
                HeldItem = "Leftovers",
                RosterOrder = 1,
                Nature = "Sassy",
                PokemonTeamId = 1,
                PokemonMoveSet = new PokemonMoveSet{
                    Move1 = "none",
                    Move2 = "none",
                    Move3 = "none",
                    Move4 = "none",
                    PkmTmId = 1
                },
                PokemonStats = new List<PokemonStat>{
                    new PokemonStat{
                        Id = 1,
                        Effort = 0,
                        Individual = 31,
                        Name = "hp",
                        Url = "url",
                        Total = 50,
                        PkmTmId = 1 
                    },
                    new PokemonStat{
                        Effort = 0,
                        Individual = 31,
                        Name = "attack",
                        Url = "url",
                        Total = 50,
                        PkmTmId = 1,
                        Id = 2
                    },
                    new PokemonStat{
                        Effort = 0,
                        Individual = 31,
                        Name = "defense",
                        Url = "url",
                        Total = 50,
                        PkmTmId = 1,
                        Id = 3
                    },
                    new PokemonStat{
                        Effort = 0,
                        Individual = 31,
                        Name = "special-attack",
                        Url = "url",
                        Total = 50,
                        PkmTmId = 1,
                        Id = 4
                    },
                    new PokemonStat{
                        Effort = 0,
                        Individual = 31,
                        Name = "special-defense",
                        Url = "url",
                        Total = 50,
                        PkmTmId = 1,
                        Id = 5
                    },
                    new PokemonStat{
                        Effort = 0,
                        Individual = 31,
                        Name = "speed",
                        Url = "url",
                        Total = 50,
                        PkmTmId = 1,
                        Id = 6
                    }
                }
        };
        _service = new PTMService(_ptmRepositoryMock.Object);
        _mockTeam = new PokemonTeam{
            Id = 1,
            Name = "mockTeam",
            TrainerId = 1,
            Trainer = new Trainer{
                Id = 1,
                Name = "mockTrainer",
                PokemonTeams = new List<PokemonTeam>()
            },
            PokemonTeamMembers = new List<PokemonTeamMember>{_ptmMock}
        };
        _mockTeamNoMembers = new PokemonTeam{
            Id = 1,
            Name = "mockTeam",
            TrainerId = 1,
            Trainer = new Trainer{
                Id = 1,
                Name = "mockTrainer",
                PokemonTeams = new List<PokemonTeam>()
            },
            PokemonTeamMembers = new List<PokemonTeamMember>()
        };
    }

    [Fact]
    public void GetAllPTMByTeamIdTest()
    {
        _ptmRepositoryMock.Setup(repository => repository.GetAllPTMByTeamId(_ptmMock.PokemonTeamId)).Returns(new List<PokemonTeamMember>(){_ptmMock});
        Assert.Contains(_ptmMock, _service.GetAllPTMByTeamId(_ptmMock.PokemonTeamId));
        _ptmRepositoryMock.Verify(repository => repository.GetAllPTMByTeamId(_ptmMock.PokemonTeamId),Times.Exactly(1));
    }

    [Fact]
    public void UpdatePTMTest()
    {
        _ptmRepositoryMock.Setup(repository => repository.GetPTMById(_ptmMock.PokemonTeamId)).Returns(_ptmMock);
        _ptmRepositoryMock.Setup(repository => repository.UpdatePTM(_ptmMock)).Returns(_ptmMock);
        Assert.Equal(_ptmMock, _service.UpdatePTM(_ptmMock));
        _ptmRepositoryMock.Verify(repository => repository.GetPTMById(_ptmMock.PokemonTeamId),Times.Exactly(1));
        _ptmRepositoryMock.Verify(repository => repository.UpdatePTM(_ptmMock),Times.Exactly(1));
    }

    [Fact]
    public void AddPkmToTeamTest()
    {
        _ptmRepositoryMock.Setup(repository => repository.GetPkmTeamById(_ptmMock.PokemonTeamId)).Returns(_mockTeamNoMembers);
        _ptmRepositoryMock.Setup(repository => repository.AddPkmToTeam(_ptmMock, _ptmMock.PokemonTeamId)).Returns(_mockTeam);
        Assert.Equal(_mockTeam, _service.AddPkmToTeam(_ptmMock, _mockTeamNoMembers.Id));
        _ptmRepositoryMock.Verify(repository => repository.GetPkmTeamById(_ptmMock.PokemonTeamId),Times.Exactly(1));
        _ptmRepositoryMock.Verify(repository => repository.AddPkmToTeam(_ptmMock, _ptmMock.PokemonTeamId),Times.Exactly(1));
    }

    [Fact]
    public void DeletePTMFromTeamTest()
    {
        Assert.Throws<NotImplementedException>(() => _service.DeletePTMFromTeam(_ptmMock));
    }

    [Fact]
    public void DeleteAllPkmFromTeamTest()
    {
        Assert.Throws<NotImplementedException>(() => _service.DeleteAllPkmFromTeam(_mockTeam.Id));
    }
}