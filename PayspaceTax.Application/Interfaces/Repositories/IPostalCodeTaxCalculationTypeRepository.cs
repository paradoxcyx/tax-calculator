using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Application.Interfaces.Repositories;

public interface IPostalCodeTaxCalculationTypeRepository
{
    /// <summary>
    /// Retrieve tax calculation type (enum) for a specified postal code
    /// </summary>
    /// <param name="postalCode">Postal code</param>
    /// <returns>Tax calculation type</returns>
    public Task<PostalCodeTaxCalculationType?> GetTaxCalculationTypeByPostalCode(string postalCode);
}