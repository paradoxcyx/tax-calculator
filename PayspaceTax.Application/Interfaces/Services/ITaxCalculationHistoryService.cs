using PayspaceTax.Application.DTOs;

namespace PayspaceTax.Application.Interfaces.Services;

public interface ITaxCalculationHistoryService
{
    /// <summary>
    /// Retrieving list of all historic tax calculations
    /// </summary>
    /// <returns>Historic tax calculations</returns>
    public Task<List<TaxCalculationHistoryDto>> GetTaxCalculationHistoryAsync();

    /// <summary>
    /// Log history for tax calculation
    /// </summary>
    /// <param name="history">The historic tax calculation</param>
    /// <returns></returns>
    public Task AddAsync(TaxCalculationHistoryDto history);
}