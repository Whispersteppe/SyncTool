using SyncTool.Classes;

namespace SyncTool.Interface
{
    public interface IActionHandlerEnumerator: IEnumerable<ISyncActionHandler>
    {
        ISyncActionHandler GetActionHandler(SyncTaskOptions syncTask);
        
        List<ISyncActionHandler> Handlers { get; }
    }
}
