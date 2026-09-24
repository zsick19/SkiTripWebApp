using Infra.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers;

// [ApiController]
// [Route("api/[controller]")]
public class UserController(UserService service) : ControllerBase
{
    [HttpGet(nameof(GetUsers))]
    public List<User> GetUsers()
    {
        return service.GetUsers();
    }
}