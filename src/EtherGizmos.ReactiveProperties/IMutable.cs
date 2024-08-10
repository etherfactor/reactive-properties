namespace EtherGizmos.ReactiveProperties;

/// <summary>
/// An object capable of being modified.
/// </summary>
public interface IMutable
{
    /// <summary>
    /// Whether or not the object is dirty.
    /// </summary>
    bool IsDirty { get; set; }

    /// <summary>
    /// The time at which the object was dirtied.
    /// </summary>
    DateTimeOffset IsDirtyTime { get; set; }

    /// <summary>
    /// The time at which the object was recalculated.
    /// </summary>
    DateTimeOffset IsRecalculatedTime { get; set; }
}
