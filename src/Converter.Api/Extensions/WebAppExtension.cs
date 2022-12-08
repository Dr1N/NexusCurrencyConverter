using System.Globalization;
using System.Text;
using Converter.Application.Contracts;
using Converter.Domain.Currency;
using Converter.Domain.Localization;
using Converter.Domain.User;

namespace Converter.Api.Extensions;

public static class WebAppExtension
{
    /// <summary>
    /// Initialize data in memory
    /// </summary>
    /// <param name="app"><see cref="WebApplication"/></param>
    public static async Task InitializeMemoryData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var ratesRepository = scope.ServiceProvider.GetService<IRepository<CurrencyRate>>();
        if (ratesRepository is not null)
        {
            await AddRatesAsync(ratesRepository);
        }
        
        var templatesRepository = scope.ServiceProvider.GetService<IRepository<ResultTemplate>>();
        if (templatesRepository is not null)
        {
            await AddTranslationsAsync(templatesRepository);
        }
    }

    /// <summary>
    /// Add default user
    /// login = admin
    /// password = password
    /// </summary>
    /// <param name="app"></param>
    public static async Task AddDefaultUserAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var userRepository = scope.ServiceProvider.GetService<IRepository<User>>();
        if (userRepository is not null)
        {
            await userRepository.AddAsync(new User("admin", "password"));
            await userRepository.SaveChangesAsync();
        }
    }
    
    private static async Task AddRatesAsync(IRepository<CurrencyRate> repository)
    {
        var sb = new StringBuilder();
        sb.AppendLine("usd:chf:0.941449");
        sb.AppendLine("usd:uah:36.924286");
        sb.AppendLine("usd:eur:0.95208");
        sb.AppendLine("usd:rub:62.00048");
        sb.AppendLine("usd:cad:1.367305");
        sb.AppendLine("usd:cny:6.972896");
        sb.AppendLine("usd:jpy:136.616444");
        sb.AppendLine("usd:inr:82.51495");

        foreach (var line in sb.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split(':', StringSplitOptions.RemoveEmptyEntries);
            var currencyOne = new Currency(parts[0]);
            var currencyTwo = new Currency(parts[1]);
            var rate = decimal.Parse(parts[2], CultureInfo.InvariantCulture);
            var currencyPairRate = new CurrencyRate(currencyOne, currencyTwo, rate);
            
            await repository.AddAsync(currencyPairRate);
        }

        await repository.SaveChangesAsync();
    }

    private static async Task AddTranslationsAsync(IRepository<ResultTemplate> repository)
    {
        var sb = new StringBuilder();
        sb.AppendLine("usd:For {0} {1} you will get {2} {3}");
        sb.AppendLine("uah:За {0} {1} ви отримаєте {2} {3}");
        sb.AppendLine("eur:For {0} {1} you will get {2} {3}");
        sb.AppendLine("rub:За {0} {1} вы получите {2} {3}");
        sb.AppendLine("cad:Pour {0} {1} vous obtiendrez {2} {3}");
        sb.AppendLine("cny:{0} {1}，您將獲得 {2} {3}");
        sb.AppendLine("jpy:{0} {1} に対して {2} {3} を取得します");
        sb.AppendLine("inr:{0} {1} के लिए आपको {2} {3} मिलेगा");

        foreach (var line in sb.ToString().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = line.Split(':');
            await repository.AddAsync(new ResultTemplate(new Currency(parts[0]), parts[1]));
        }

        await repository.SaveChangesAsync();
    }
}
