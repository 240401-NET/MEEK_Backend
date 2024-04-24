This is how we setup the project so far.

dotnet new webapi --name PokemonTeamBuilder.API -o ./PokemonTeamBuilder.API
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.Tools

Create initial user secrets vault 
dotnet user-secrets init
dotnet user-secrets list
dotnet user-secrets set {Name of secret} {value of secret}
dotnet user-secrets remove {name of secret}

Azure connection string flatten:

dotnet user-secrets set {ConnectionStrings:{name of db}} {ConnectionString}

builder.Services.AddDbContext<DBContext>(options =>
     options.UseSqlServer(builder.Configuration.GetConnectionString("name of db")));

To run:
dotnet run

Implementing a new feature:
git checkout -b nameOfMybranch
--do coding work
git add {dir,files, or all}
git commit -m "commit message"
git push --set-upstream origin nameOfMybranch