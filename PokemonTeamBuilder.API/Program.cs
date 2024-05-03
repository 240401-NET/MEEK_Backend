using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Repository;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(co => {
    co.AddPolicy("name" , pb =>{
        pb.WithOrigins("http://localhost:5173", "http://otherlocalhost:port")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<UserDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UserID_Local")));

builder.Services.AddDbContext<PokemonTrainerDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PSTBAPI_Local")));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPKMTeamService, PKMTeamServices>();
builder.Services.AddScoped<IPKMTeamRepo, PKMTeamRepository>();

builder.Services.AddAuthorization();
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<UserDBContext>()
.AddSignInManager<SignInManager<IdentityUser>>();

builder.Services.AddHttpClient();

builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddAuthentication()
//     .AddBearerToken(IdentityConstants.BearerScheme);

// builder.Services.AddAuthorizationBuilder()
//     .AddPolicy("api", p => {
//         p.RequireAuthenticatedUser();
//         p.AddAuthenticationSchemes(IdentityConstants.BearerScheme);
//     });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<IdentityUser>();
app.UseCors("name");
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();















/* Weather API that came with the project
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
*/
