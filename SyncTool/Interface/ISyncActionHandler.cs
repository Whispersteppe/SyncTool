using SyncTool.Classes;

namespace SyncTool.Interface;

public interface ISyncActionHandler
{
    bool CanExecute(SyncTaskOptions options);
    Task Execute(SyncTaskOptions options);
}
