using SyncTool.Classes;
using SyncTool.Enums;
using SyncTool.Interface;

namespace SyncTool.Handler;

/// <summary>
/// mirror handler
/// </summary>
/// <remarks>
/// mirror is a one way copy, including inserts, deletes, and updates
/// </remarks>
public class MirrorHandler : ISyncActionHandler
{
    public bool CanExecute(SyncTaskOptions options)
    {
        return options.Action == ActionType.Mirror;
    }

    public async Task Execute(SyncTaskOptions options)
    {
        throw new NotImplementedException();
    }
}
