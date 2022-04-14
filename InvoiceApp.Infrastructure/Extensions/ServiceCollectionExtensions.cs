using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InvoiceApp.Common.Options;
using InvoiceApp.Common.Repositories;

namespace InvoiceApp.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSettings(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<PostgresConfiguration>(configuration.GetSection(nameof(PostgresConfiguration)));

        return services;
    }

    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

        return services;
    }
}