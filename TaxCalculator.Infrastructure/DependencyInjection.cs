using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TaxCalculator.Application.Interfaces.Repositories;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Infrastructure.Database;
using TaxCalculator.Infrastructure.Repositories;
using TaxCalculator.Infrastructure.Seeder;

namespace TaxCalculator.Infrastructure;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });
        
        DbSeeder.SeedData(services);
        
        services.AddScoped<IRepository<PostalCodeTaxCalculationType>, Repository<PostalCodeTaxCalculationType>>();
        services.AddScoped<IRepository<ProgressiveTaxBracket>, Repository<ProgressiveTaxBracket>>();
        services.AddScoped<IRepository<TaxCalculationHistory>, Repository<TaxCalculationHistory>>();
    }
}