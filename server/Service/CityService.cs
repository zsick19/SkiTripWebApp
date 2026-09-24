using System;
using Infra;
using Infra.Models;

namespace Service;

public class CityService(SkiTripContext dbContext)
{

    public List<CityLocation> GetCurrentCities()
    {
        return dbContext.CityLocations.ToList();
    }

    public List<CityLocation> GetFutureCities()
    {
        throw new NotImplementedException();
    }
}
