using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PokemonTeamBuilder.API.DB;

public class UserDBContext : IdentityDbContext
{
    public UserDBContext() : base() {}
    public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) {}

    //public DbSet<IdentityUser> Users{ get; set; }
}