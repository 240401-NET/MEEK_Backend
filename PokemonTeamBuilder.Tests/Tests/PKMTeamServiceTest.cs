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
    PokemonTeamMember postMockTM;
    PokemonTeamMember preMockTM;
    TeamMemberDTO ptmDTO;
    PokemonTeam mockTeam;
    PokemonTeam mockTeamNoMembers;
    PokemonPokeApi mockPokemonPokeAPI;
    string _pkmAPIBaseUrl = "https://pokeapi.co/api/v2/pokemon/";
    PokemonTeamDTO pkmTeam;
    int trainerId = 1;
    public PKMTeamServiceTest(){
        ptmServiceMock = new();
        teamRepoMock = new();
        pkmAPIServiceMock = new();
        postMockTM = new PokemonTeamMember{
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
        preMockTM = new PokemonTeamMember{
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
                    PkmTmId = 1
                },
                new PokemonStat{
                    Effort = 0,
                    Individual = 31,
                    Name = "defense",
                    Url = "url",
                    Total = 50,
                    PkmTmId = 1
                },
                new PokemonStat{
                    Effort = 0,
                    Individual = 31,
                    Name = "special-attack",
                    Url = "url",
                    Total = 50,
                    PkmTmId = 1
                },
                new PokemonStat{
                    Effort = 0,
                    Individual = 31,
                    Name = "special-defense",
                    Url = "url",
                    Total = 50,
                    PkmTmId = 1
                },
                new PokemonStat{
                    Effort = 0,
                    Individual = 31,
                    Name = "speed",
                    Url = "url",
                    Total = 50,
                    PkmTmId = 1
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
            PokemonTeamMembers = new List<PokemonTeamMember>{postMockTM}
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
        pkmTeam = new PokemonTeamDTO{
            Name = "mockTeam",
            PokemonTeamMembers = new List<TeamMemberDTO>(){ptmDTO}
        };
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
        
        ptmServiceMock.Setup(service => service.AddPkmToTeam(It.Is<PokemonTeamMember>(data => data.Name == preMockTM.Name), 1)).Returns(mockTeam);
        teamRepoMock.Setup(repo => repo.CreateNewTeam(It.Is<PokemonTeam>(data => data.Name == mockTeam.Name && data.TrainerId == trainerId))).Returns(mockTeamNoMembers);
        pkmAPIServiceMock.Setup(service => service.GetPokemonFromAPI(_pkmAPIBaseUrl + preMockTM.PkmApiId)).Returns(Task.FromResult(mockPokemonPokeAPI));
        PKMTeamServices service = new PKMTeamServices(teamRepoMock.Object, pkmAPIServiceMock.Object, ptmServiceMock.Object);
        
        Assert.NotNull(service.CreateNewTeam(pkmTeam, 1));
        ptmServiceMock.Verify(service => service.AddPkmToTeam(It.Is<PokemonTeamMember>(data => data.Name == preMockTM.Name), 1), Times.Exactly(pkmTeam.PokemonTeamMembers.Count));
        teamRepoMock.Verify(repository => repository.CreateNewTeam(It.Is<PokemonTeam>(data => data.Name == mockTeam.Name && data.TrainerId == trainerId)), Times.Exactly(1));
        pkmAPIServiceMock.Verify(service => service.GetPokemonFromAPI(_pkmAPIBaseUrl + preMockTM.PkmApiId),Times.Exactly(pkmTeam.PokemonTeamMembers.Count));
    }
    
}