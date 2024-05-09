using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Repository;
using PokemonTeamBuilder.API.DTO;
using Moq;
using System;
using Xunit;
using System.Data.Common;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace PokemonTeamBuilder.Tests;

public class PKMTeamServiceTest
{
    Mock<IPTMService> ptmServiceMock;
    Mock<IPKMTeamRepo> teamRepoMock;
    Mock<IPKMAPISevice> pkmAPIServiceMock;
    PokemonTeamMember ptmMock;
    TeamMemberDTO ptmDTO;
    PokemonTeam mockTeam;
    PokemonTeam mockTeamNoMembers;
    PokemonPokeApi mockPokemonPokeAPI;
    string _pkmAPIBaseUrl = "https://pokeapi.co/api/v2/pokemon/";
    PokemonTeamDTO pkmTeamDTO;
    int trainerId = 1;
    PKMTeamServices service;

    public PKMTeamServiceTest(){
        ptmServiceMock = new();
        teamRepoMock = new();
        pkmAPIServiceMock = new();
        ptmMock = new PokemonTeamMember{
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
        ptmDTO = new TeamMemberDTO{
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
        mockTeam = new PokemonTeam{
            Id = 1,
            Name = "mockTeam",
            TrainerId = 1,
            Trainer = new Trainer{
                Id = 1,
                Name = "mockTrainer",
                PokemonTeams = new List<PokemonTeam>()
            },
            PokemonTeamMembers = new List<PokemonTeamMember>{ptmMock}
        };
        mockTeamNoMembers = new PokemonTeam{
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
        mockPokemonPokeAPI = new PokemonPokeApi{
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
        pkmTeamDTO = new PokemonTeamDTO{
            Name = "mockTeam",
            PokemonTeamMembers = new List<TeamMemberDTO>(){ptmDTO}
        };
        service = new PKMTeamServices(teamRepoMock.Object, pkmAPIServiceMock.Object, ptmServiceMock.Object);
    }

    [Fact]
    public void Test()
    {
        //Arrange

        //Act

        //Assert
    }

    [Fact]
    public void CreateNewTeamTest(){
        
        ptmServiceMock.Setup(service => service.AddPkmToTeam(It.Is<PokemonTeamMember>(data => data.Name == ptmMock.Name), 1)).Returns(mockTeam);
        teamRepoMock.Setup(repository => repository.CreateNewTeam(It.Is<PokemonTeam>(data => data.Name == mockTeam.Name && data.TrainerId == trainerId))).Returns(mockTeamNoMembers);
        pkmAPIServiceMock.Setup(service => service.GetPokemonFromAPI(_pkmAPIBaseUrl + ptmMock.PkmApiId)).Returns(Task.FromResult(mockPokemonPokeAPI));
        
        Assert.NotNull(service.CreateNewTeam(pkmTeamDTO, 1));
        ptmServiceMock.Verify(service => service.AddPkmToTeam(It.Is<PokemonTeamMember>(data => data.Name == ptmMock.Name), 1), Times.Exactly(pkmTeamDTO.PokemonTeamMembers.Count));
        teamRepoMock.Verify(repository => repository.CreateNewTeam(It.Is<PokemonTeam>(data => data.Name == mockTeam.Name && data.TrainerId == trainerId)), Times.Exactly(1));
        pkmAPIServiceMock.Verify(service => service.GetPokemonFromAPI(_pkmAPIBaseUrl + ptmMock.PkmApiId),Times.Exactly(pkmTeamDTO.PokemonTeamMembers.Count));
    }

    [Fact]
    public async void GetTeamByIdTest(){
        teamRepoMock.Setup(repository => repository.GetTeam(mockTeam.Id)).Returns(Task.FromResult(mockTeam));
        teamRepoMock.Setup(repository => repository.DoesTeamExist(mockTeam.Id)).Returns(Task.FromResult(true));
        
        Assert.NotNull(await service.GetTeam(mockTeam.Id));
        teamRepoMock.Verify(repository => repository.GetTeam(mockTeam.Id), Times.Exactly(1));
        teamRepoMock.Verify(repository => repository.DoesTeamExist(mockTeam.Id), Times.Exactly(1));
    }

    [Fact]
    public async void GetTeamByNameTest(){
        teamRepoMock.Setup(repository => repository.GetTeam(mockTeam.Name)).Returns(Task.FromResult(mockTeam));
        teamRepoMock.Setup(repository => repository.DoesTeamExist(mockTeam.Name)).Returns(Task.FromResult(true));
        
        Assert.NotNull(await service.GetTeam(mockTeam.Name));
        teamRepoMock.Verify(repository => repository.GetTeam(mockTeam.Name), Times.Exactly(1));
        teamRepoMock.Verify(repository => repository.DoesTeamExist(mockTeam.Name), Times.Exactly(1));
    }

    [Fact]
    public void GetAllTest(){
        teamRepoMock.Setup(repository => repository.GetAll(mockTeam.TrainerId)).Returns(new List<PokemonTeam>(){mockTeam});
        
        Assert.Contains(mockTeam, service.GetAll(mockTeam.TrainerId));
        teamRepoMock.Verify(repository => repository.GetAll(mockTeam.TrainerId), Times.Exactly(1));
    }

    [Fact]
    public void UpdateTeamTest(){
        pkmTeamDTO.Id = mockTeam.Id;
        teamRepoMock.Setup(repository => repository.UpdateTeam(It.Is<PokemonTeam>(data => data.Name == pkmTeamDTO.Name))).Returns(mockTeam);
        teamRepoMock.Setup(repository => repository.GetAllTeamId(mockTeam.TrainerId)).Returns(new List<int>(){mockTeam.Id});
        teamRepoMock.Setup(repository => repository.GetTeam(mockTeam.Id)).Returns(Task.FromResult(mockTeam));
        
        Assert.Equal(mockTeam, service.UpdateTeam(pkmTeamDTO, mockTeam.TrainerId));
        teamRepoMock.Verify(repository => repository.GetTeam(mockTeam.Id), Times.Exactly(1));
        teamRepoMock.Verify(repository => repository.GetAllTeamId(mockTeam.TrainerId), Times.Exactly(1));
        teamRepoMock.Verify(repository => repository.UpdateTeam(It.Is<PokemonTeam>(data => data.Name == pkmTeamDTO.Name)), Times.Exactly(1));
        pkmTeamDTO.Id = null;
    }

    [Fact]
    public void DeleteTeamTest(){
        teamRepoMock.Setup(repository => repository.DeleteTeam(mockTeam.Id)).Returns(mockTeam);
        teamRepoMock.Setup(repository => repository.GetAllTeamId(mockTeam.TrainerId)).Returns(new List<int>(){mockTeam.Id});
        
        Assert.Equal(mockTeam, service.DeleteTeam(mockTeam.TrainerId, mockTeam.Id));
        teamRepoMock.Verify(repository => repository.DeleteTeam(mockTeam.Id), Times.Exactly(1));
        teamRepoMock.Verify(repository => repository.GetAllTeamId(mockTeam.TrainerId), Times.Exactly(1));
    }
}