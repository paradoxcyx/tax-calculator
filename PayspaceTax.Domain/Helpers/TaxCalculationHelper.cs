using PayspaceTax.Domain.Consts;

namespace PayspaceTax.Domain.Helpers;

public class TaxCalculationHelper
{
    /// <summary>
    /// Calculating the progressive tax according to rules specified
    /// </summary>
    /// <param name="income">Annual Income</param>
    /// <param name="ratePercentage">Rate Percentage</param>
    /// <returns>Calculated Tax</returns>
    public virtual decimal CalculateProgressiveTax(decimal income, decimal ratePercentage)
    {
        return Math.Round(income * (ratePercentage/100),2);
    }

    /// <summary>
    /// Calculating the Flat Value tax according to rules specified
    /// </summary>
    /// <param name="income">Annual Income</param>
    /// <returns></returns>
    public virtual decimal CalculateFlatValueTax(decimal income)
    {
        return income < TaxConsts.FlatValueThreshold ? 
            Math.Round(income * (TaxConsts.FlatValuePercentage/100),2) : 
            Math.Round(TaxConsts.FlatValueFixedAmount,2);
    }   

    /// <summary>
    /// Calculating the Flat Rate tax according to rules specified
    /// </summary>
    /// <param name="income">Annual income</param>
    /// <returns>Calculated tax</returns>
    public virtual decimal CalculateFlatRateTax(decimal income)
    {
        return Math.Round(income * (TaxConsts.FlatRatePercentage/100),2);
    }
}