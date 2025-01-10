using SyncTool.Classes;
using SyncTool.Enums;
using SyncTool.Interface;
using System.Diagnostics;

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
        else if (filePair.FromFileInfo.LastWriteTime == filePair.ToFileInfo.LastWriteTime)
        {
            //  times are the same
            return;
        }
        else if (filePair.FromFileInfo.Length == filePair.ToFileInfo.Length)
        {
            Debug.WriteLine($"From Hash Code: {filePair.FromFileInfo.GetHashCode()} To Hash Code: {filePair.ToFileInfo.GetHashCode()}");

            //  we'll skip if the filelengths are the same
            return;
        }
        // TODO should do a file compare here

        //  back up the file, replacing the current, and renaming the old To file
        string initialPath = filePair.ToFileInfo.FullName;
        string backupExtension = Path.GetExtension(initialPath);
        string backupPath = Path.GetDirectoryName(initialPath);
        string backupFileName = Path.GetFileNameWithoutExtension(initialPath);
        string backupDate = filePair.ToFileInfo.LastWriteTime.ToString("yyyyMMddHHmmss");

        //string backupFullFileName = backupPath + @"\" + backupFileName + '.' + backupDate + backupExtension;
        string backupFullFileName = $@"{backupPath}\{backupFileName}.{backupDate}{backupExtension}";

        Debug.WriteLine(backupFullFileName);


        filePair.ToFileInfo.MoveTo(backupFullFileName);
        filePair.FromFileInfo.CopyTo(initialPath);

    }
}
