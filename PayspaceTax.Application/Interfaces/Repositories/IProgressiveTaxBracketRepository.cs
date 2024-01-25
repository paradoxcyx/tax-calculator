using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Application.Interfaces.Repositories;

public interface IProgressiveTaxBracketRepository
{
    /// <summary>
    /// Get all progressive tax brackets
    /// </summary>
    /// <returns>progressive tax brackets</returns>
    public Task<List<ProgressiveTaxBracket>> GetProgressiveTaxBracketsAsync();

    /// <summary>
    /// Get progressive tax bracket by income
    /// </summary>
    /// <param name="annualIncome">Annual income</param>
    /// <returns>progressive tax bracket</returns>
    public Task<ProgressiveTaxBracket?> GetProgressiveTaxBracketByIncomeAsync(decimal annualIncome);
}