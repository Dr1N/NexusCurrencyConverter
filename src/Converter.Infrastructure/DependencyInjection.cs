using Converter.Application.Contracts;
using Converter.Infrastructure.Authentication;
using Converter.Infrastructure.Repositories.InMemory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Converter.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services)
    {
        services.AddScoped(
            typeof(IRepository<>),
            typeof(MemoryRepository<>));
        
        return services;
    }

    public static IServiceCollection AddJwtRsaAuth(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtSettings = new JwtSettings();
        configuration.GetSection(JwtSettings.SectionName).Bind(jwtSettings);
        var jwtHandler = new JwtHandler(jwtSettings);
        services.AddSingleton<IJwtHandler>(jwtHandler);
        services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = jwtHandler.TokenValidationParameters);

        return services;
    }
}
