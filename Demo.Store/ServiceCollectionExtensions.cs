using Demo.Abstractions;
using Demo.Store;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddStoreServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<ICandidateProductStore, CandidateProductStore>();
        return services;
    }
}
