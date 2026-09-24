using System;

namespace Infra.Models;

public class StorageLocation
{
    public Guid StorageLocationId { get; set; }
    public string StorageLocationName { get; set; } = string.Empty;
    public int SkiStorageCapacity { get; set; }

    public List<SkiGear> StoredGear { get; set; } = new();
}
