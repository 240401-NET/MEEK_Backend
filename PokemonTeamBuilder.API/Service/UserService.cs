using Microsoft.AspNetCore.Identity;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Service;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> GetCurrentUser()
    {
        
        throw new NotImplementedException();
    }

    public async Task<IdentityResult> RegisterUser(RegisterUser registration)
    {
        ApplicationUser user = new()
        {
            UserName = registration.Username,
            Email = registration.Email,
            SecurityStamp = Guid.NewGuid().ToString()
        };

        var result = await _userManager.CreateAsync(user, registration.Password);

        return result;
    }

    public async Task<SignInResult> LoginUser(LoginUser login, bool? useCookies, bool? useSessionCookies)
    {
        var useCookieScheme = (useCookies == true) || (useSessionCookies == true);
        var isPersistent = (useCookies == true) && (useSessionCookies != true);
        _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

        var result = await _signInManager.PasswordSignInAsync(login.Username, login.Password, isPersistent, lockoutOnFailure: true);

        return result;
    }

    public async void Logout()
    {
        await _signInManager.SignOutAsync();
    }
}