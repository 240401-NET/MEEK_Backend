using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PokemonTeamBuilder.API.Data;

public class UserDBContext : IdentityDbContext
{
    public UserDBContext() : base() {}
    public UserDBContext(DbContextOptions options) : base(options) {}

    //public DbSet<IdentityUser> Users{ get; set; }
}