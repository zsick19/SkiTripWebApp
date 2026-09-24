using System;
using Infra.Models;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CityController(CityService service) : ControllerBase
{
    [HttpGet(nameof(GetCurrentCities))]
    public List<CityLocation> GetCurrentCities()
    {
        return service.GetCurrentCities();
    }


    [HttpGet(nameof(GetFutureCities))]
    public List<CityLocation> GetFutureCities()
    {
        return service.GetFutureCities();
    }


    [HttpPost(nameof(CreateCityLocation))]
    public void CreateCityLocation()
    {
        throw new NotImplementedException();
    }


}
