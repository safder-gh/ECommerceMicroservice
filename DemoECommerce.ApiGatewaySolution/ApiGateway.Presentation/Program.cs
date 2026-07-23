using Ocelot.Cache.CacheManager;
using Ocelot.DependencyInjection;
using ecommerce.SharedLibrary.DependencyInjection;
using Microsoft.Extensions.Options;
using ApiGateway.Presentation.Middleware;
using Ocelot.Middleware;
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("ocelot.json",optional:false,reloadOnChange:true);
builder.Services.AddOcelot().AddCacheManager(o=>o.WithDictionaryHandle());

JWTAuthenticationSchem.AddJWTAuthenticationSchem(builder.Services, builder.Configuration);
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyHeader().
        AllowAnyMethod().
        AllowAnyOrigin();
    });
});
var app = builder.Build();
app.UseHttpsRedirection();
app.UseCors();
app.UseMiddleware<AttacheSignatureToRequest>();
app.UseOcelot().Wait();
app.Run();

