## This is how we setup the project so far.
- dotnet new webapi --name PokemonTeamBuilder.API -o ./PokemonTeamBuilder.API
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.Design
- dotnet add package Microsoft.EntityFrameworkCore.Tools

## ASPNET Identity Packages
- dotnet add package Microsoft.AspNetCore.Identity.EntityFrameworkCore

## Create initial user secrets vault 
- dotnet user-secrets init
- dotnet user-secrets list
- dotnet user-secrets set {Name of secret} {value of secret}
- dotnet user-secrets remove {name of secret}

## Azure connection string flatten
- dotnet user-secrets set {ConnectionStrings:{name of db}} {ConnectionString}

## Adding DBcontext for database
- builder.Services.AddDbContext<DBContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("name of db")));

## To run
- dotnet run

## Implementing a new feature
- git pull
- git checkout -b nameOfMybranch
- *do coding work
- git add {dir,files, or all}
- git commit -m "commit message"
- git push --set-upstream origin nameOfMybranch

## To get back to main
- git checkout main

## To make migrations to DB
Ensure you have user-secrets with proper connection strings setup with the names used on line 11 and 14 of Program.cs
Make sure the connection string {Initial Catalog} field is different for {UserDBContext} and {PokemonTrainerDbContext} otherwise all tables will be made into the same database

#### Examples of my local DB Connection strings
- ConnectionStrings:UserID_Local = Server=localhost;Database=master;Initial Catalog=UserIdentityDB;Trusted_Connection=True;TrustServerCertificate=True;
- ConnectionStrings:PSTBAPI_Local = Server=localhost;Database=master;Initial Catalog=PSTBAPI;Trusted_Connection=True;TrustServerCertificate=True;

#### Run the below commands to make migrations
- dotnet ef database update --context PokemonTrainerDbContext
- dotnet ef database update --context UserDBContext 