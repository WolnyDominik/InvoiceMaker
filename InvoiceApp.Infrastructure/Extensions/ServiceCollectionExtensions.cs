using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using InvoiceApp.Common.Options;
using InvoiceApp.Common.Repositories;
using InvoiceApp.Domain.Automapper;
using InvoiceApp.Infrastructure.Services;

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

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IHashService, HashService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();

        return services;
    }

    public static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
    {
        services.AddSingleton(AutomapperConfiguration.Build());

        return services;
    }
}
