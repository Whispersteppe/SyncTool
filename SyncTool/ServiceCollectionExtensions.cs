using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace SyncTool;

public static class ServiceCollectionExtensions
{
    public static ServiceCollection AddTypesOfInterface<T>( this ServiceCollection services, Assembly assembly)
    {
        var items = assembly
            .GetTypes()
            .Where(x => x.IsAssignableTo(typeof(T)))
            .Where(x => !x.IsAbstract)
            .ToList();

        items.ForEach(x => services.AddScoped(typeof(T), x));

        return services;
    }
    public static ServiceCollection AddTypesOfInterface<T>(this ServiceCollection services, Type? typeInSourceAssembly = null)
    {
        var assembly = Assembly.GetAssembly(typeInSourceAssembly ?? typeof(T)) 
            ?? throw new InvalidOperationException("Cannot get assembly for the given type");

        return AddTypesOfInterface<T>(services, assembly);
    }
}
