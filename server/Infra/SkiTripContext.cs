using Infra.Models;
using Microsoft.EntityFrameworkCore;

namespace Infra;

public class SkiTripContext : DbContext
{

    public DbSet<User> Users => Set<User>();

    public DbSet<CityLocation> CityLocations => Set<CityLocation>();
    public DbSet<SkiGear> SkiGears => Set<SkiGear>();

    public DbSet<SkiTrip> SkiTrips => Set<SkiTrip>();
    public DbSet<StorageLocation> StorageLocations => Set<StorageLocation>();

    // 1. Standard constructor used at application runtime by your ASP.NET Core API
    public SkiTripContext(DbContextOptions<SkiTripContext> options) : base(options)
    {
    }

    // 2. Pure parameterless constructor used strictly by EF Core CLI tooling during migrations
    public SkiTripContext() : base()
    {
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Fallback fallback configuration strictly for migrations tooling
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=SkiTrip.db");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<SkiTrip>()
            .HasMany(t => t.RegisteredUsers)
            .WithMany(u => u.AttendingTrips);

        modelBuilder.Entity<SkiTrip>()
            .HasOne(t => t.TripCoordinator)
            .WithMany(u => u.CoordinatedTrips)
            .HasForeignKey(t => t.TripCoordinatorId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }

}

public static class DbInitializer
{
    public static void Initialize(SkiTripContext context)
    {
        context.Database.EnsureCreated();

        // Check if data already exists
        if (context.Users.Any()) return;

        // 1. Seed Storage Locations
        var breckStorage = new StorageLocation { StorageLocationId = Guid.NewGuid(), StorageLocationName = "Breckenridge Base Locker A", SkiStorageCapacity = 50 };
        var vailStorage = new StorageLocation { StorageLocationId = Guid.NewGuid(), StorageLocationName = "Vail East Lodge Locker B", SkiStorageCapacity = 30 };
        context.StorageLocations.AddRange(breckStorage, vailStorage);

        // 2. Seed Users (Admin, Coordinator, Standard)
        var adminUser = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = "Sarah",
            LastName = "Miller",
            PhoneNumber = "303-555-0143",
            HomeAddress = "123 Aspen Way",
            AddressCity = "Denver",
            AddressState = "CO",
            AddressZipCode = "80202",
            Role = UserRole.Admin
        };

        var coordinatorUser = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = "James",
            LastName = "Wilson",
            PhoneNumber = "720-555-0199",
            HomeAddress = "567 Pine Lane",
            AddressCity = "Boulder",
            AddressState = "CO",
            AddressZipCode = "80301",
            Role = UserRole.Coordinator
        };

        var standardUser = new User
        {
            UserId = Guid.NewGuid(),
            FirstName = "Alex",
            LastName = "Smith",
            PhoneNumber = "970-555-0122",
            HomeAddress = "789 Maple Court",
            AddressCity = "Fort Collins",
            AddressState = "CO",
            AddressZipCode = "80521",
            Role = UserRole.Standard
        };
        context.Users.AddRange(adminUser, coordinatorUser, standardUser);

        // 3. Seed City Locations
        var denverHub = new CityLocation
        {
            CityLocationId = Guid.NewGuid(),
            CityName = "Denver Central Hub",
            IsActive = true,
            CityDirectorId = adminUser.UserId
        };
        context.CityLocations.Add(denverHub);

        // 4. Seed Ski Gear
        var gear1 = new SkiGear { SkiGearId = Guid.NewGuid(), GearName = "Salomon QST 92 Skis", IsStoredOnSite = true, GearOwnerId = standardUser.UserId, StorageLocationId = breckStorage.StorageLocationId };
        var gear2 = new SkiGear { SkiGearId = Guid.NewGuid(), GearName = "Burton Custom Snowboard", IsStoredOnSite = false, GearOwnerId = coordinatorUser.UserId };
        context.SkiGears.AddRange(gear1, gear2);

        // 5. Seed Ski Trips
        var upcomingTrip = new SkiTrip
        {
            SkiTripId = Guid.NewGuid(),
            TripTitle = "Early Season Breckenridge Run",
            IsPastTrip = false,
            TripOriginCity = "Denver",
            TripOriginLocation = "Union Station Pick-up",
            TripDestinationLocation = "Breckenridge Gondola Lot",
            NumberOfSeats = 12,
            TripOriginDepartureTime = DateTime.UtcNow.AddDays(14).Date.AddHours(6), // 6:00 AM
            TripOriginArrivalTime = DateTime.UtcNow.AddDays(14).Date.AddHours(8.5), // 8:30 AM
            TripDestinationDepartureTime = DateTime.UtcNow.AddDays(14).Date.AddHours(16.5), // 4:30 PM
            TripDestinationArrivalTime = DateTime.UtcNow.AddDays(14).Date.AddHours(19), // 7:00 PM
            IsRoundTrip = true,
            TotalMiles = 160,
            TotalCost = 45.00,
            TripCoordinatorId = coordinatorUser.UserId
        };

        // Add relationship associations
        upcomingTrip.RegisteredUsers.Add(standardUser);
        upcomingTrip.RegisteredUsers.Add(adminUser);

        context.SkiTrips.Add(upcomingTrip);

        // Save all changes to local SQLite database
        context.SaveChanges();
    }
}
