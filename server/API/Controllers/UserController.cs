using Infra;
using Infra.DTOs;
using Infra.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers;

// [ApiController]
// [Route("api/[controller]")]
public class UserController(UserService service) : ControllerBase
{
    [HttpGet(nameof(GetUsers))]
    public List<User> GetUsers(int page = 1, int resultsPerPage = 10)
    {
        return service.GetUsers(page, resultsPerPage);
    }

    [HttpGet(nameof(GetUserDetailsById))]
    public List<UserGearDTO> GetUserDetailsById(int userId)
    {
        return service.GetUserDetailsById(userId);
    }

    [HttpPost(nameof(CreateUser))]
    public User CreateUser(CreateUserRequestDto userDto)
    {
        return service.CreateUser(userDto);
    }
}
