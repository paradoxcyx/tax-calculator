using Microsoft.EntityFrameworkCore;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Domain.Entities;
using PayspaceTax.Infrastructure.Database;

namespace PayspaceTax.Infrastructure.Repositories;

public class TaxCalculationHistoryRepository(AppDbContext dbContext) : ITaxCalculationHistoryRepository
{

    public async Task AddAsync(TaxCalculationHistory history)
    {
        await dbContext.TaxCalculationHistories.AddAsync(history);
        await dbContext.SaveChangesAsync();
    }

    public async Task<List<TaxCalculationHistory>> GetAllAsync()
    {
        return await dbContext.TaxCalculationHistories.ToListAsync();
    }
}