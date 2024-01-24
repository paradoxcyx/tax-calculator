using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Application.Interfaces.Repositories;

public interface IProgressiveTaxBracketRepository
{
    public Task<List<ProgressiveTaxBracket>> GetProgressiveTaxBracketsAsync();

    public Task<ProgressiveTaxBracket?> GetProgressiveTaxBracketByIncomeAsync(decimal annualIncome);
}