using Microsoft.Extensions.DependencyInjection;
using SyncTool.Interface;

namespace SyncTool;

public class ServiceProviderFactory
{
    readonly ServiceCollection _collection;
    readonly ServiceProvider _provider;

    public ServiceProviderFactory()
    {
        _collection = new ServiceCollection();

        _collection.AddTypesOfInterface<ISyncActionHandler>();
        _collection.AddScoped<IActionHandlerEnumerator, ActionHandlerEnumerator>();

        _provider = _collection.BuildServiceProvider();
    }

    public ServiceProvider Provider => _provider;
}
