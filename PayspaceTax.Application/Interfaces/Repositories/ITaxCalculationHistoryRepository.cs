using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Application.Interfaces.Repositories;

public interface ITaxCalculationHistoryRepository
{
    /// <summary>
    /// Add new record for tax that has been calculated
    /// </summary>
    /// <param name="history"></param>
    /// <returns></returns>
    public Task AddAsync(TaxCalculationHistory history);
    
    /// <summary>
    /// Retrieving all historic tax calculations
    /// </summary>
    /// <returns>Historic tax calculations</returns>
    public Task<List<TaxCalculationHistory>> GetAllAsync();
}