using System;

namespace Infra.Models;

public class SkiGear
{

    public Guid SkiGearId { get; set; }
    public string GearName { get; set; } = string.Empty;
    public bool IsStoredOnSite { get; set; }

    // Explicit Foreign Keys & Navigation Properties
    public Guid GearOwnerId { get; set; }
    public User GearOwner { get; set; } = null!;

    public Guid? StorageLocationId { get; set; } // Nullable if not stored on-site
    public StorageLocation? StorageLocation { get; set; }

}
