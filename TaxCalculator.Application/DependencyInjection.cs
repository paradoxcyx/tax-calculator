using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Application.Interfaces.Services;
using TaxCalculator.Application.Services;

namespace TaxCalculator.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITaxCalculationService, TaxCalculationService>();
        services.AddScoped<ITaxCalculationHistoryService, TaxCalculationHistoryService>();
    }
}