using Microsoft.Extensions.DependencyInjection;
using SyncTool.Classes;
using SyncTool.Interface;

namespace SyncTool;

public delegate void FileInformation(string filePath);
public delegate void FolderInformation(string folderPath);

public class SyncRunner
{

    readonly ServiceProviderFactory _providerBuilder;
    readonly ServiceProvider _provider;

    public FileInformation? OnFileStartCopy;
    public FileInformation? OnFileCopyError;
    public FileInformation? OnFileCopySuccess;

    public FileInformation? OnFileCopyNew;
    public FileInformation? OnFileCopyDelete;
    public FileInformation? OnFileCopyMatch;
    public FileInformation? OnFileCopyOverwrite;

    public FolderInformation? OnFolderStartCopy;
    public FolderInformation? OnFolderCopyError;
    public FolderInformation? OnFolderCopySuccess;



    public SyncRunner()
    {
        _providerBuilder = new ServiceProviderFactory();
        _provider = _providerBuilder.Provider;
    }

    /// <summary>
    /// run a series of tasks in sequence
    /// </summary>
    /// <param name="job"></param>
    public async Task Execute(SyncJobOptions job)
    {
        foreach(SyncTaskOptions task in job.Tasks)
        {
            await Execute(task);
        }
    }

    /// <summary>
    /// run a single task
    /// </summary>
    /// <param name="task"></param>
    public async Task Execute(SyncTaskOptions task) 
    {
        IActionHandlerSet? handlerSet = _provider.GetService<IActionHandlerSet>() 
            ?? throw new InvalidOperationException("cannot get the IActionHandlerSet");

        foreach (var handler in handlerSet.Handlers)
        {
            if (handler.CanExecute(task))
            {
                await handler.Execute(task);
                break;
            }
        }
    }
}
