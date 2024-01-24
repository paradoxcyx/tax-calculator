using Microsoft.Extensions.DependencyInjection;
using PayspaceTax.Domain.Helpers;

namespace PayspaceTax.Domain;

public static class DependencyInjection
{
    public static void AddDomain(this IServiceCollection services)
    {
        services.AddScoped<ITaxCalculationHelper, TaxCalculationHelper>();
    }
}