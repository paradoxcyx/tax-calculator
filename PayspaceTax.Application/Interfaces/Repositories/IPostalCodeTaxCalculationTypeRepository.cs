using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Application.Interfaces.Repositories;

public interface IPostalCodeTaxCalculationTypeRepository
{
    public Task<PostalCodeTaxCalculationType?> GetTaxCalculationTypeByPostalCode(string postalCode);
}