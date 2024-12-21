using SyncTool.Classes;
using System.IO;

namespace SyncTool.Handler;

public class BaseHandler
{
    public async Task StartHandler(SyncPairOptions pairOptions)
    {
        DirectoryMatchPair directoryPair = new DirectoryMatchPair()
        {
            FromDirectoryInfo = new DirectoryInfo(pairOptions.FromPath),
            ToDirectoryInfo = new DirectoryInfo(pairOptions.ToPath)
        };

        await DirectoryProcess(pairOptions, directoryPair);
    }

    public virtual async Task DirectoryProcess(SyncPairOptions pairOptions, DirectoryMatchPair parentPair)
    {
        if (!await CanProcessDirectory(parentPair))
        {
            return;
        }

        await OnBeforeDirectoryProcess(parentPair);

        string toDirectoryPath = Path.GetFullPath(parentPair.ToDirectoryInfo.FullName);

        DirectoryPairEnumerator directoryEmitter = new DirectoryPairEnumerator(parentPair);

        foreach(var childPair in directoryEmitter)
        {
            await DirectoryProcess(pairOptions, childPair);
        }


        OnDirectoryBeforeFileProcess(parentPair);

        FilePairEnumerator fileEmitter = new FilePairEnumerator(parentPair);
        foreach(var childPair in fileEmitter)
        {
            if (await CanProcessFile(childPair))
            {
                await OnFileProcess(childPair);
            }
        }

        await OnDirectoryAfterFileProcess(parentPair);

        await OnAfterDirectoryProcess(parentPair);
    }
    public virtual async Task OnDirectoryBeforeFileProcess(DirectoryMatchPair directoryPair) { }
    public virtual async Task OnDirectoryAfterFileProcess(DirectoryMatchPair directoryPair) { }
    public virtual async Task<bool> CanProcessFile(FileMatchPair filePair) { return false; }
    public virtual async Task OnFileProcess(FileMatchPair filePair) { }

    public virtual async Task<bool> CanProcessDirectory(DirectoryMatchPair directoryPair) { return false; }
    public virtual async Task OnBeforeDirectoryProcess(DirectoryMatchPair directoryPair) { }
    public virtual async Task OnAfterDirectoryProcess(DirectoryMatchPair directoryPair) { }

}
