using Microsoft.OpenApi.Models;

namespace ProxyServerDocusignAPI;

public static class SwaggerConfig
{
    public static void AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Reverse Proxy Server API",
                Version = "v1",
                Description = "API para gestionar recursos de DocuSign",
                Contact = new OpenApiContact
                {
                    Name = "Eric Ruiz",
                    Email = "eric19rr@outlook.es"
                }
            });
        });
    }
}