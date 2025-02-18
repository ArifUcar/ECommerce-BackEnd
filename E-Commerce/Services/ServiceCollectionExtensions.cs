using E_Commerce.Services.Implementations;
using E_Commerce.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace E_Commerce.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // HTTP istemcilerini yapılandır
        services.AddHttpClient<IProductService, ProductService>();
        services.AddHttpClient<ICategoryService, CategoryService>();
        services.AddHttpClient<IAuthService, AuthService>();

        // Servisleri kaydet
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IAuthService, AuthService>();

        return services;
    }
} 