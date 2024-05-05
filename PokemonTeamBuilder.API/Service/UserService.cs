using Microsoft.AspNetCore.Identity;
using PokemonTeamBuilder.API.Model;
using PokemonTeamBuilder.API.Repository;

namespace PokemonTeamBuilder.API.Service;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly ITrainerService _trainerService;
    private readonly IUserRepository _userRepository;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ITrainerService trainerService, IUserRepository userRepository)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _trainerService = trainerService;
        _userRepository = userRepository;
    }

    public void CreateTrainerForUser(string username)
    {
        Trainer newtrainer = _trainerService.CreateTrainer(username);
        _userRepository.AddTrainerToUser(username, newtrainer);
    }

    public async Task<IdentityResult> RegisterUser(RegisterUser registration)
    {
        ApplicationUser user = new()
        {
            UserName = registration.Username,
            Email = registration.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
        };

        var result = await _userManager.CreateAsync(user, registration.Password);
        return result;
    }

    public async Task<SignInResult> LoginUser(LoginUser login)
    {
        // var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
        // var isPersistent = (useCookies == true) && (useSessionCookies != true);
        _signInManager.AuthenticationScheme = IdentityConstants.ApplicationScheme;

        var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, isPersistent: true, lockoutOnFailure: true);

        return result;
    }

    public async void Logout()
    {
        await _signInManager.SignOutAsync();
    }

    public ApplicationUser? GetUserByName(string username)
    {
        var user = _userRepository.GetUserByName(username);
        return user;
    }
}