using System.Reflection;
using Converter.Application.Services.Converter;
using Converter.Application.Services.User;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Converter.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICurrencyConverterService, CurrencyConverterService>();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        return services;
    }
}
