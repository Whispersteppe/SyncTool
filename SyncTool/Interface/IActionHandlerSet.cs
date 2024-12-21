using SyncTool.Classes;

namespace SyncTool.Interface
{
    public interface IActionHandlerSet
    {
        ISyncActionHandler GetActionHandler(SyncTaskOptions syncTask);
        
        List<ISyncActionHandler> Handlers { get; }
    }
}
