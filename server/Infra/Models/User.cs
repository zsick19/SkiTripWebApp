namespace Infra.Models;

public enum UserRole
{
    Standard,
    Coordinator,
    Admin
}
public class User
{
    public Guid UserId { get; set; }

    // Fixed: Changed from fields to auto-properties so EF Core tracks them
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string HomeAddress { get; set; } = string.Empty;
    public string AddressCity { get; set; } = string.Empty;
    public string AddressState { get; set; } = string.Empty;
    public string AddressZipCode { get; set; } = string.Empty;

    public UserRole Role { get; set; }

    // Navigation Properties
    public List<SkiTrip> AttendingTrips { get; set; } = new();
    public List<SkiTrip> CoordinatedTrips { get; set; } = new();
    public List<SkiGear> PersonalGear { get; set; } = new();

}

