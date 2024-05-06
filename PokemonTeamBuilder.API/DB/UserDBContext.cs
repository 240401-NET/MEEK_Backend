using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.DB;

public class UserDBContext : IdentityDbContext<ApplicationUser>
{
    public UserDBContext() : base() {}
    public UserDBContext(DbContextOptions<UserDBContext> options) : base(options) {}

    public virtual DbSet<ApplicationUser> AppUsers{ get; set; }
}