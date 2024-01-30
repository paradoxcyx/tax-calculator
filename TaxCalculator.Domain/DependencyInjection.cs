using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Domain.Helpers;

namespace TaxCalculator.Domain;

public static class DependencyInjection
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<TaxCalculationHelper>();
    }
}