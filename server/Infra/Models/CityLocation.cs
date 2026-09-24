using System;

namespace Infra.Models;

public class CityLocation
{
    public Guid CityLocationId { get; set; }
    public string CityName { get; set; } = string.Empty;
    public bool IsActive { get; set; }

    // Explicit Foreign Key for the Director
    public Guid? CityDirectorId { get; set; }
    public User? CityDirector { get; set; }
}
