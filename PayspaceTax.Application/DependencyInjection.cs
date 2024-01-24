using Microsoft.Extensions.DependencyInjection;
using PayspaceTax.Application.Interfaces.Services;
using PayspaceTax.Application.Services;

namespace PayspaceTax.Application;

public static class DependencyInjection
{
    public static void AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITaxCalculationService, TaxCalculationService>();
    }
}