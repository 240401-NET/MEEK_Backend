using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.DTO;
using Moq;

namespace PokemonTeamBuilder.Tests;

public class PKMTeamServiceTest
{
    Mock<IPTMService> _ptmServiceMock;
    Mock<IPKMTeamRepo> _teamRepoMock;
    Mock<IPKMAPISevice> _pkmAPIServiceMock;
    PokemonTeamMember _ptmMock;
    TeamMemberDTO _ptmDTO;
    PokemonTeam _mockTeam;
    PokemonTeam _mockTeamNoMembers;
    PokemonPokeApi _mockPokemonPokeAPI;
    string _pkmAPIBaseUrl = "https://pokeapi.co/api/v2/pokemon/";
    PokemonTeamDTO _pkmTeamDTO;
    int _trainerId = 1;
    PKMTeamServices _service;

    public PKMTeamServiceTest(){
        _ptmServiceMock = new();
        _teamRepoMock = new();
        _pkmAPIServiceMock = new();
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
        _ptmDTO = new TeamMemberDTO{
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
            PokemonMoveSet = new PokemonMoveSetDTO{
                Move1 = "none",
                Move2 = "none",
                Move3 = "none",
                Move4 = "none"
            },
            PokemonStats = new List<PokemonStatDTO>{
                new PokemonStatDTO{
                    Effort = 0,
                    Individual = 31,
                    Name = "hp"
                },
                new PokemonStatDTO{
                    Effort = 0,
                    Individual = 31,
                    Name = "attack"
                },
                new PokemonStatDTO{
                    Effort = 0,
                    Individual = 31,
                    Name = "defense"
                },
                new PokemonStatDTO{
                    Effort = 0,
                    Individual = 31,
                    Name = "special-attack"
                },
                new PokemonStatDTO{
                    Effort = 0,
                    Individual = 31,
                    Name = "special-defense"
                },
                new PokemonStatDTO{
                    Effort = 0,
                    Individual = 31,
                    Name = "speed"
                }
            }
        };
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
        _mockPokemonPokeAPI = new PokemonPokeApi{
            Id = 1,
            Name = "bulbasaur",
            PokemonBaseStats = new List<PokemonBaseStat>{
                new PokemonBaseStat{
                    Name = "hp",
                    BaseStat = 10,
                    Url = "url",
                    PkmApiId = 1
                },
                new PokemonBaseStat{
                    Id = 2, 
                    Name = "attack",
                    BaseStat = 10,
                    Url = "url",
                    PkmApiId = 1
                },
                new PokemonBaseStat{
                    Id = 3, 
                    Name = "defense",
                    BaseStat = 10,
                    Url = "url",
                    PkmApiId = 1
                },
                new PokemonBaseStat{
                    Id = 4, 
                    Name = "special-attack",
                    BaseStat = 10,
                    Url = "url",
                    PkmApiId = 1
                },
                new PokemonBaseStat{
                    Id = 5, 
                    Name = "special-defense",
                    BaseStat = 10,
                    Url = "url",
                    PkmApiId = 1
                },
                new PokemonBaseStat{
                    Id = 6, 
                    Name = "speed",
                    BaseStat = 10,
                    Url = "url",
                    PkmApiId = 1
                }
            }
        };
        _pkmTeamDTO = new PokemonTeamDTO{
            Name = "mockTeam",
            PokemonTeamMembers = new List<TeamMemberDTO>(){_ptmDTO}
        };
        _service = new PKMTeamServices(_teamRepoMock.Object, _pkmAPIServiceMock.Object, _ptmServiceMock.Object);
    }

    [Fact]
    public void CreateNewTeamTest(){
        
        _ptmServiceMock.Setup(service => service.AddPkmToTeam(It.Is<PokemonTeamMember>(data => data.Name == _ptmMock.Name), 1)).Returns(_mockTeam);
        _teamRepoMock.Setup(repository => repository.CreateNewTeam(It.Is<PokemonTeam>(data => data.Name == _mockTeam.Name && data.TrainerId == _trainerId))).Returns(_mockTeamNoMembers);
        _pkmAPIServiceMock.Setup(service => service.GetPokemonFromAPI(_pkmAPIBaseUrl + _ptmMock.PkmApiId)).Returns(Task.FromResult(_mockPokemonPokeAPI));
        
        Assert.NotNull(_service.CreateNewTeam(_pkmTeamDTO, 1));
        _ptmServiceMock.Verify(service => service.AddPkmToTeam(It.Is<PokemonTeamMember>(data => data.Name == _ptmMock.Name), 1), Times.Exactly(_pkmTeamDTO.PokemonTeamMembers.Count));
        _teamRepoMock.Verify(repository => repository.CreateNewTeam(It.Is<PokemonTeam>(data => data.Name == _mockTeam.Name && data.TrainerId == _trainerId)), Times.Exactly(1));
        _pkmAPIServiceMock.Verify(service => service.GetPokemonFromAPI(_pkmAPIBaseUrl + _ptmMock.PkmApiId),Times.Exactly(_pkmTeamDTO.PokemonTeamMembers.Count));
    }

    [Fact]
    public async void GetTeamByIdTest(){
        _teamRepoMock.Setup(repository => repository.GetTeam(_mockTeam.Id)).Returns(Task.FromResult(_mockTeam));
        _teamRepoMock.Setup(repository => repository.DoesTeamExist(_mockTeam.Id)).Returns(Task.FromResult(true));
        
        Assert.NotNull(await _service.GetTeam(_mockTeam.Id));
        _teamRepoMock.Verify(repository => repository.GetTeam(_mockTeam.Id), Times.Exactly(1));
        _teamRepoMock.Verify(repository => repository.DoesTeamExist(_mockTeam.Id), Times.Exactly(1));
    }

    [Fact]
    public async void GetTeamByNameTest(){
        _teamRepoMock.Setup(repository => repository.GetTeam(_mockTeam.Name)).Returns(Task.FromResult(_mockTeam));
        _teamRepoMock.Setup(repository => repository.DoesTeamExist(_mockTeam.Name)).Returns(Task.FromResult(true));
        
        Assert.NotNull(await _service.GetTeam(_mockTeam.Name));
        _teamRepoMock.Verify(repository => repository.GetTeam(_mockTeam.Name), Times.Exactly(1));
        _teamRepoMock.Verify(repository => repository.DoesTeamExist(_mockTeam.Name), Times.Exactly(1));
    }

    [Fact]
    public void GetAllTest(){
        _teamRepoMock.Setup(repository => repository.GetAll(_mockTeam.TrainerId)).Returns(new List<PokemonTeam>(){_mockTeam});
        
        Assert.Contains(_mockTeam, _service.GetAll(_mockTeam.TrainerId));
        _teamRepoMock.Verify(repository => repository.GetAll(_mockTeam.TrainerId), Times.Exactly(1));
    }

    [Fact]
    public void UpdateTeamTest(){
        _pkmTeamDTO.Id = _mockTeam.Id;
        _teamRepoMock.Setup(repository => repository.UpdateTeam(It.Is<PokemonTeam>(data => data.Name == _pkmTeamDTO.Name))).Returns(_mockTeam);
        _teamRepoMock.Setup(repository => repository.GetAllTeamId(_mockTeam.TrainerId)).Returns(new List<int>(){_mockTeam.Id});
        _teamRepoMock.Setup(repository => repository.GetTeam(_mockTeam.Id)).Returns(Task.FromResult(_mockTeam));
        
        Assert.Equal(_mockTeam, _service.UpdateTeam(_pkmTeamDTO, _mockTeam.TrainerId));
        _teamRepoMock.Verify(repository => repository.GetTeam(_mockTeam.Id), Times.Exactly(1));
        _teamRepoMock.Verify(repository => repository.GetAllTeamId(_mockTeam.TrainerId), Times.Exactly(1));
        _teamRepoMock.Verify(repository => repository.UpdateTeam(It.Is<PokemonTeam>(data => data.Name == _pkmTeamDTO.Name)), Times.Exactly(1));
        _pkmTeamDTO.Id = null;
    }

    [Fact]
    public void DeleteTeamTest(){
        _teamRepoMock.Setup(repository => repository.DeleteTeam(_mockTeam.Id)).Returns(_mockTeam);
        _teamRepoMock.Setup(repository => repository.GetAllTeamId(_mockTeam.TrainerId)).Returns(new List<int>(){_mockTeam.Id});
        
        Assert.Equal(_mockTeam, _service.DeleteTeam(_mockTeam.TrainerId, _mockTeam.Id));
        _teamRepoMock.Verify(repository => repository.DeleteTeam(_mockTeam.Id), Times.Exactly(1));
        _teamRepoMock.Verify(repository => repository.GetAllTeamId(_mockTeam.TrainerId), Times.Exactly(1));
    }
}