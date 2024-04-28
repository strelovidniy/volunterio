using EntityFrameworkCore.RepositoryInfrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Volunterio.Data.Context;
using Volunterio.Data.Entities;

namespace Volunterio.Data.DependencyInjection;

public static class DataDependencyInjectionExtension
{
    public static IServiceCollection RegisterDataLayer(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services
        .AddDbContext(configuration)
        .AddRepositories();

    private static IServiceCollection AddDbContext(
        this IServiceCollection services,
        IConfiguration configuration
    ) => services
        .AddDbContext<VolunterioContext>(options =>
        {
            options
                .UseNpgsql(configuration.GetConnectionString("Volunterio"));

            #if DEBUG
            options
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors();
            #endif
        });

    private static IServiceCollection AddRepositories(
        this IServiceCollection services
    ) => services
        .CreateRepositoryBuilderWithContext<VolunterioContext>()
        .AddRepository<Address>()
        .AddRepository<ContactInfo>()
        .AddRepository<HelpRequest>()
        .AddRepository<HelpRequestImage>()
        .AddRepository<NotificationSettings>()
        .AddRepository<PushSubscription>()
        .AddRepository<Role>()
        .AddRepository<User>()
        .AddRepository<UserDetails>()
        .Build();
}