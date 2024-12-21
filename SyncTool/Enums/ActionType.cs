namespace SyncTool.Enums;

/// <summary>
/// the set of actions the SyncTool is capable of doing
/// </summary>
public enum ActionType
{
    /// <summary>
    /// the unknown state
    /// </summary>
    Unknown = 0,
    /// <summary>
    /// One way copy.  no deletes, could overwrite the target if changed.
    /// </summary>
    Backup,
    /// <summary>
    /// two way sync.  inserts, changes, and deletes
    /// </summary>
    Sync,
    /// <summary>
    /// one way copy.  deletes and inserts, and overwrites on the targets
    /// </summary>
    Mirror,
    /// <summary>
    /// generate information about the two directories
    /// </summary>
    Information,
}
