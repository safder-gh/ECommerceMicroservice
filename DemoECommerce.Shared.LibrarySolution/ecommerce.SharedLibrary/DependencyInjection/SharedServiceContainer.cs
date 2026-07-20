using ecommerce.SharedLibrary.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

namespace ecommerce.SharedLibrary.DependencyInjection;

public static class SharedServiceContainer
{
    public static IServiceCollection AddSharedServices<TContext>(this IServiceCollection services,IConfiguration configuration,string fileName) where TContext:DbContext
    {
        services.AddDbContext<TContext>(option => option.UseSqlServer(configuration.GetConnectionString("eCommerceConnection"),sqlOption=>sqlOption.EnableRetryOnFailure()));
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.Debug()
            .WriteTo.Console()
            .WriteTo.File(path: $"{fileName}-.text",
            restrictedToMinimumLevel: LogEventLevel.Information,
            outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {message:lj}{newLine} {Exception}",
            rollingInterval: RollingInterval.Day)
            .CreateLogger();
        JWTAuthenticationSchem.AddJWTAuthenticationSchem(services, configuration);
        return services;
    }
    public static IApplicationBuilder UseSharedPolicies(this IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseMiddleware<GlobalException>();
        applicationBuilder.UseMiddleware<ListenToOnlyAPIGateway>();
        return applicationBuilder;
    }
}
