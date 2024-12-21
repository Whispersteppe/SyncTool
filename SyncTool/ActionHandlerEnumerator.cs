using SyncTool.Classes;
using SyncTool.Interface;
using System.Collections;

namespace SyncTool;

/// <summary>
/// the set of handlers for the different actions
/// </summary>
/// <remarks>
/// this class won't be directly created, but it will be available through the ServiceProvider from the ServiceProviderFactory
/// </remarks>
public class ActionHandlerEnumerator : IActionHandlerEnumerator
{
    readonly List<ISyncActionHandler> _actions;

    /// <summary>
    /// construct an action handler
    /// </summary>
    /// <param name="handlers">the set of handlers.  these are DI'd into the class</param>
    /// 
    public ActionHandlerEnumerator(IEnumerable<ISyncActionHandler> handlers) 
    {
        _actions = new List<ISyncActionHandler> (handlers);
    }

    /// <summary>
    /// get the list of handlers
    /// </summary>
    public List<ISyncActionHandler> Handlers
    {
        get 
        { 
            return _actions; 
        }
    }

    /// <summary>
    /// select a particular action handler by whether it can handle the current request or not.
    /// </summary>
    /// <param name="syncTask"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public ISyncActionHandler GetActionHandler(SyncTaskOptions syncTask)
    {
        foreach (ISyncActionHandler handler in _actions)
        {
            if (handler.CanExecute(syncTask))
            {
                return handler;
            }
        }

        throw new InvalidOperationException($"Cannot find a handler for {syncTask.Name}");
    }

    public IEnumerator<ISyncActionHandler> GetEnumerator()
    {
        foreach(var handler in _actions)
        {
            yield return handler;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
