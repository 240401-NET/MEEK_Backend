using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace PokemonTeamBuilder.API.Model;

public class User : IdentityUser
{
    [Required]
    public string CustomUserName { get; set;} = string.Empty;
}