using PayspaceTax.Application.DTOs;

namespace PayspaceTax.Application.Interfaces.Services;

public interface ITaxCalculationService
{
    /// <summary>
    /// Calculating tax based on type for postal code
    /// </summary>
    /// <param name="calculateTax">The input for calculating tax</param>
    /// <returns>The calculated tax</returns>
    public Task<CalculateTaxDto> CalculateTaxAsync(CalculateTaxDto calculateTax);
}