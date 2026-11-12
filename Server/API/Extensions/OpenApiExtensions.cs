using NSwag;
using NSwag.Generation.Processors.Security;

public static class OpenApiExtensions
{
    public static IServiceCollection AddOpenApiDocumentation(
        this IServiceCollection services)
    {
        services.AddOpenApiDocument(document =>
        {
            document.DocumentProcessors.Add(
                new SecurityDefinitionAppender("Bearer",
                    new OpenApiSecurityScheme
                    {
                        Type = OpenApiSecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        Description = "Enter your JWT bearer token"
                    }));

            document.OperationProcessors.Add(
                new AspNetCoreOperationSecurityScopeProcessor("Bearer"));
        });

        return services;
    }
}