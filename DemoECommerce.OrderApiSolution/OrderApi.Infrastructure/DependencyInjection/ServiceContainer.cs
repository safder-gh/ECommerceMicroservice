using ecommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OrderApi.Infrastructure.Data;
using OrderApi.Infrastructure.Repositories;
using ProductApi.Application.Interfaces;

namespace OrderApi.Infrastructure.DependencyInjection;

public  static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        SharedServiceContainer.AddSharedServices<OrderDBContext>(services, configuration, configuration["MySerilog:FileName"]!);
        services.AddScoped<IOrderRepository, OrderRepository>();
        return services;
    }

    public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder builder)
    {
        SharedServiceContainer.UseSharedPolicies(builder);
        return builder;
    }
}
