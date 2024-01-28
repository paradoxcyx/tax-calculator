using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Infrastructure.Database;
using PayspaceTax.Infrastructure.Repositories;
using PayspaceTax.Infrastructure.Seeder;
using PayspaceTax.Domain.Entities;
namespace PayspaceTax.Infrastructure;

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