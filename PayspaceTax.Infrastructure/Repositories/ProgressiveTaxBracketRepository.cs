using Microsoft.EntityFrameworkCore;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Domain.Entities;
using PayspaceTax.Infrastructure.Database;

namespace PayspaceTax.Infrastructure.Repositories;

public class ProgressiveTaxBracketRepository(AppDbContext dbContext) : IProgressiveTaxBracketRepository
{
    private readonly AppDbContext _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

    public async Task<List<ProgressiveTaxBracket>> GetProgressiveTaxBracketsAsync()
    {
        return await _dbContext.ProgressiveTaxBrackets.ToListAsync();
    }

    public async Task<ProgressiveTaxBracket?> GetProgressiveTaxBracketByIncomeAsync(decimal annualIncome)
    {
        var progressiveTaxBracket =
            await _dbContext.ProgressiveTaxBrackets.FirstOrDefaultAsync(x =>
                (x.To.HasValue && x.From <= annualIncome && x.To >= annualIncome) ||
                (!x.To.HasValue && x.From <= annualIncome));

        return progressiveTaxBracket;
    }
}