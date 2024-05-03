using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PokemonTeamBuilder.API.Model;

public class ApplicationUser : IdentityUser
{
    public string? TrainerId { get; set; }
}