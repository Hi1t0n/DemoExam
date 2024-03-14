using Demo.Domain;
using Demo.Infrastructure.DbContext;
using Demo.Infrastructure.Helpers;
using Demo.Infrastructure.Managers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Demo.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessLogic(this IServiceCollection services, string connectionString) =>
        services
            .AddDatabase(connectionString)
            .AddManager();

    public static IServiceCollection AddDatabase(this IServiceCollection services, string connectionString)
    {
        return services.AddDbContext<ApplicationDbContext>(builder => builder.UseNpgsql(connectionString));
    }

    public static IServiceCollection AddManager(this IServiceCollection services)
    {
        services.AddScoped<IAuthManager, AuthManager>();
        services.AddScoped<IStatementManager, StatementManager>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<ICheckData, CheckData>();
        return services;
    }
}