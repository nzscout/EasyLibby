using EasyLibby.Api.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EasyLibby.Api.Data;

public static class DataExtensions
{
    public static async Task InitializeDbAsync(this IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<LibraryDbContext>();
        await dbContext.Database.MigrateAsync();

        var logger = serviceProvider.GetRequiredService<ILoggerFactory>()
                                    .CreateLogger("DB Initializer");
        logger.LogInformation(5, "The database is ready!");
    }

    public static IServiceCollection AddRepositories(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connString = configuration.GetConnectionString("DefaultConnection");

        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

        services.AddDbContext<LibraryDbContext>(options => options.UseSqlServer(connString));

        return services;
    }
}