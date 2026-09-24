using Infra;
using Infra.Models;

namespace Service;

public class UserService(SkiTripContext dbContext)
{
    public List<User> GetUsers()
    {
        return dbContext.Users.ToList();
    }

}