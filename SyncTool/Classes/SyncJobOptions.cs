namespace SyncTool.Classes;

public class SyncJobOptions
{
    public int ConcurrentTaskCount { get; set; } = 1;
    public List<SyncTaskOptions> Tasks { get; set; } = [];
}
