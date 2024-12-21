using SyncTool.Classes;
using SyncTool.Enums;
using SyncTool.Interface;

namespace SyncTool.Handler;

/// <summary>
/// handler for a sync action
/// </summary>
/// <remarks>
/// sync is a two-way copy, including deletes, inserts, and changes
/// </remarks>
public class SyncHandler : ISyncActionHandler
{
    public bool CanExecute(SyncTaskOptions options)
    {
        return options.Action == ActionType.Sync;
    }

    public async Task Execute(SyncTaskOptions options)
    {
        throw new NotImplementedException();
    }
}
