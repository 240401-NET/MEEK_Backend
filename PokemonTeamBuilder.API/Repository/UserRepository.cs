using PokemonTeamBuilder.API.DB;
using PokemonTeamBuilder.API.Model;

namespace PokemonTeamBuilder.API.Repository;

public class UserRepository : IUserRepository
{
    private readonly UserDBContext _userDBContext;

    public UserRepository(UserDBContext userDBContext)
    {
        _userDBContext = userDBContext;
    }

    public void AddTrainerToUser(string username, Trainer trainer)
    {        
       ApplicationUser user = _userDBContext.AppUsers.FirstOrDefault(user => user.UserName!.Equals(username))!;
       user.TrainerId = trainer.Id;
       _userDBContext.SaveChanges();

       Console.WriteLine(user);
    }

    public ApplicationUser? GetUserByName(string username)
    {
        return _userDBContext.AppUsers.FirstOrDefault(user => user.UserName!.Equals(username));
    }

    public int GetTrainerId(string username)
    {
        var user = GetUserByName(username);

        if(user is not null)
        {
            return (int)user.TrainerId!;
        }

        return 0;
    }
}