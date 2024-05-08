using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Service;
using PokemonTeamBuilder.API.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(co => {
    co.AddPolicy("name" , pb =>{
        pb.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
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
builder.Services.AddScoped<IPKMAPIService, PKMAPIService>();
builder.Services.AddScoped<IPTMService, PTMService>();
builder.Services.AddScoped<ITrainerService, TrainerService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPKMTeamRepo, PKMTeamRepository>();
builder.Services.AddScoped<IPKMAPIRepository, PKMAPIRepository>();
builder.Services.AddScoped<IPTMRepository, PTMRepository>();
builder.Services.AddScoped<ITrainerRepository, TrainerRepository>();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 8;
})
.AddEntityFrameworkStores<UserDBContext>()
.AddSignInManager<SignInManager<ApplicationUser>>();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddHttpClient();

builder.Services.AddControllers()
.AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.MapIdentityApi<IdentityUser>();
app.UseCors("name");
app.UseAuthentication();
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
