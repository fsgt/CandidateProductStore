using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Demo.App;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = Host.CreateDefaultBuilder(args);

        builder.ConfigureServices((ctx, services) =>
        {
            services.AddStoreServices(ctx.Configuration);
            services.AddDeverythingApiServices(ctx.Configuration);

            services.AddHostedService<DemoHostedService>();
        });

        var app = builder.Build();

        app.Run();
    }
}
