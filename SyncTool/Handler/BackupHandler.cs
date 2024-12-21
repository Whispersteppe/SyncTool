using SyncTool.Classes;
using SyncTool.Enums;
using SyncTool.Interface;

namespace SyncTool.Handler;

/// <summary>
/// backup handler
/// </summary>
/// <remarks>
/// backup is a one way copy like mirroring, but no deletes or overwrites.  
/// we'll need options on what to do if a collision occurs.
/// </remarks>
public class BackupHandler : BaseHandler, ISyncActionHandler
{
    public bool CanExecute(SyncTaskOptions options)
    {
        return options.Action == ActionType.Backup;
    }

    public async Task Execute(SyncTaskOptions options)
    {
        foreach (var syncPair in options.SyncPairs)
        {
            await StartHandler(syncPair);
        }
    }

    public override async Task<bool> CanProcessDirectory(DirectoryMatchPair directoryPair)
    {
        if (directoryPair.FromDirectoryInfo.Exists == false)
        {
            // nothing to worry about.  
            return false;
        }

        return true;
    }

    public override async Task OnBeforeDirectoryProcess(DirectoryMatchPair directoryPair)
    {

        if (directoryPair.ToDirectoryInfo.Exists == false)
        {
            //  create the to directory
            directoryPair.ToDirectoryInfo.Create();
            return;
        }

    }

    public override async Task<bool> CanProcessFile(FileMatchPair filePair)
    {
        if (filePair.FromFileInfo.Exists == false)
        {
            // nothing to worry about.  
            return false;
        }

        return true;
    }
    public override async Task OnFileProcess(FileMatchPair filePair)
    {

        if (filePair.ToFileInfo.Exists == false)
        {
            //  copy the file
            filePair.FromFileInfo.CopyTo(filePair.ToFileInfo.FullName);
            return;
        }
    }
}
