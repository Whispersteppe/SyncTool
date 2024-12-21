using SyncTool.Enums;

namespace SyncTool.Classes;

/// <summary>
/// The set of options for a sync operation
/// </summary>
public class SyncTaskOptions
{
    public string Name { get; set; } = "";
    public ActionType Action { get; set; } = ActionType.Unknown;

    public List<SyncPairOptions> SyncPairs { get; set; } = [];
}

public class SyncTaskBackup : SyncTaskOptions
{
    public FileHitAction FileHitAction { get; set; }
    public bool DoIncremental { get; set; }
    public bool ZipData { get; set; }
}

public class SyncTaskSync : SyncTaskOptions
{
    public string MismatchHandling { get; set; } = "";
}

public enum FileHitAction
{
    Overwrite,
    VersionOld,
    DontOverwrite
}