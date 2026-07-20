using ecommerce.SharedLibrary.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductApi.Application.Interfaces;
using ProductApi.Infrastructure.Data;
using ProductApi.Infrastructure.Repositories;

namespace ProductApi.Infrastructure.DependencyInjection;

public  static class ServiceContainer
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
    {
        SharedServiceContainer.AddSharedServices<ProductDBContext>(services, configuration, configuration["MySerilog:FileName"]!);
        services.AddScoped<IProductRepository, ProductRepository>();
        return services;
    }

    public static IApplicationBuilder UseInfrastructurePolicy(this IApplicationBuilder builder)
    {
        SharedServiceContainer.UseSharedPolicies(builder);
        return builder;
    }
}
