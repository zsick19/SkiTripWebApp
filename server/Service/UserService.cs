using Infra;
using Infra.DTOs;
using Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace Service;

public class UserService(SkiTripContext dbContext)
{
    public List<User> GetUsers(int page, int resultsPerPage)
    {
        return dbContext.Users.Skip((page - 1) * resultsPerPage).Take(resultsPerPage)
        .ToList();
    }

    public List<UserGearDTO> GetUserDetailsById(int userId)
    {
        return dbContext.Users.Include(u => u.PersonalGear)
        .Select(UserGearDTO.Projection)
        .ToList();
    }

    public User CreateUser(CreateUserRequestDto userDto)
    {
        if(string.IsNullOrWhiteSpace(userDto.FirstName) || string.IsNullOrWhiteSpace(userDto.LastName) || string.IsNullOrWhiteSpace(userDto.Email))
        {
            throw new ArgumentException("First name, last name, and email are required.");
        }
        var user = new User
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            Email = userDto.Email,
            PhoneNumber = userDto.PhoneNumber,
            HomeAddress = userDto.HomeAddress,
            AddressCity = userDto.AddressCity,
            AddressState = userDto.AddressState,
            AddressZipCode = userDto.AddressZipCode
        };
        dbContext.Users.Add(user);

        dbContext.SaveChanges();
        return user;
    }
}

