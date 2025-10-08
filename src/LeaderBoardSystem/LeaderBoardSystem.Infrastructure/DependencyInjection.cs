using LeaderboardSystem.Application.Common.Interfaces;
using LeaderboardSystem.Infrastructure.Persistence;
using LeaderboardSystem.Infrastructure.Redis;
using LeaderboardSystem.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LeaderboardSystem.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider =>
                provider.GetRequiredService<ApplicationDbContext>());

            // Redis
            var redisConnection = configuration.GetConnectionString("Redis")
                ?? throw new InvalidOperationException("Redis connection string not configured");

            services.AddSingleton(new RedisConnectionFactory(redisConnection));
            services.AddSingleton<IRedisService, RedisService>();

            // Services
            services.AddScoped<ILeaderboardService, LeaderboardService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddTransient<IDateTime, DateTimeService>();

            return services;
        }
    }
}
