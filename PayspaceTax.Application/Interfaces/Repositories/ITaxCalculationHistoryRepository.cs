using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Application.Interfaces.Repositories;

public interface ITaxCalculationHistoryRepository
{
    public Task AddAsync(TaxCalculationHistory history);
    public Task<List<TaxCalculationHistory>> GetAllAsync();
}