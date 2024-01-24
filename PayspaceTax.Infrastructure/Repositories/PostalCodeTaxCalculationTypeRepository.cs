using Microsoft.EntityFrameworkCore;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Domain.Entities;
using PayspaceTax.Infrastructure.Database;

namespace PayspaceTax.Infrastructure.Repositories;

public class PostalCodeTaxCalculationTypeRepository(AppDbContext dbContext) : IPostalCodeTaxCalculationTypeRepository
{
    public async Task<PostalCodeTaxCalculationType?> GetTaxCalculationTypeByPostalCode(string postalCode)
    {
        var taxCalculationType =
            await dbContext.PostalCodeTaxCalculationTypes.FirstOrDefaultAsync(x => x.PostalCode == postalCode);

        return taxCalculationType;
    }
}