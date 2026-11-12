using Microsoft.Extensions.DependencyInjection;
using Service.Security;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServiceLayer(
        this IServiceCollection services,
        JwtSettings jwtSettings)
    {
        // Configuration
        services.AddSingleton(jwtSettings);

        // Security
        services.AddScoped<ITokenService, JwtTokenService>();
        services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();

        // Application services
        services.AddScoped<UserService>();
        services.AddScoped<ProductService>();
        services.AddScoped<CategoryService>();
        services.AddScoped<OrderService>();

        return services;
    }
}