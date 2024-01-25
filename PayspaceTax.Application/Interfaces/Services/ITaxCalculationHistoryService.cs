using PayspaceTax.Application.DTOs;

namespace PayspaceTax.Application.Interfaces.Services;

public interface ITaxCalculationHistoryService
{
    /// <summary>
    /// Retrieving list of all historic tax calculations
    /// </summary>
    /// <returns>Historic tax calculations</returns>
    public Task<List<TaxCalculationHistoryDto>> GetTaxCalculationHistoryAsync();
}