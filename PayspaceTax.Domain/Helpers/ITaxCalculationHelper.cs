namespace PayspaceTax.Domain.Helpers;

public interface ITaxCalculationHelper
{
    public decimal CalculateProgressiveTax(decimal income, decimal ratePercentage);
    public decimal CalculateFlatValueTax(decimal income);
    public decimal CalculateFlatRateTax(decimal income);
}