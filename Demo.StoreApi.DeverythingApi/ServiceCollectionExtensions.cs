using Demo.StoreApi.Abstractions;
using Demo.StoreApi.DeverythingApi;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDeverythingApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICandidateProductStoreApi, DeverythingProductStoreApi>();
        return services;
    }
}
