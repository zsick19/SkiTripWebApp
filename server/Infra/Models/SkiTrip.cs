namespace Infra.Models;

public class SkiTrip
{
    public Guid SkiTripId { get; set; }
    public string TripTitle { get; set; } = string.Empty;
    public bool IsPastTrip { get; set; }
    public string TripOriginCity { get; set; } = string.Empty;
    public string TripOriginLocation { get; set; } = string.Empty;
    public string TripDestinationLocation { get; set; } = string.Empty;
    public int NumberOfSeats { get; set; }
    public DateTime TripOriginDepartureTime { get; set; }
    public DateTime TripOriginArrivalTime { get; set; }
    public DateTime TripDestinationDepartureTime { get; set; }
    public DateTime TripDestinationArrivalTime { get; set; }
    public bool IsRoundTrip { get; set; }
    public int TotalMiles { get; set; }
    public double TotalCost { get; set; }

    // Explicit Foreign Key for Coordinator
    public Guid? TripCoordinatorId { get; set; }
    public User? TripCoordinator { get; set; }

    // Many-to-Many relationship mapping back to attendees
    public List<User> RegisteredUsers { get; set; } = new();
}
