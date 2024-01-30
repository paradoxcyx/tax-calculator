using TaxCalculator.Domain.Consts;

namespace TaxCalculator.Domain.Helpers;

public class TaxCalculationHelper
{
    /// <summary>
    /// Calculate the progressive tax amount taken into account the annual income and rate percentage
    /// </summary>
    /// <param name="income">Annual income</param>
    /// <param name="ratePercentage">Rate percentage</param>
    /// <returns>Tax amount</returns>
    public virtual decimal CalculateProgressiveTax(decimal income, decimal ratePercentage)
    {
        return Math.Round(income * (ratePercentage/100),2);
    }
    
    /// <summary>
    /// Calculate the Flat Value tax for annual income
    /// </summary>
    /// <param name="income">Annual income</param>
    /// <returns>Tax amount</returns>
    public virtual decimal CalculateFlatValueTax(decimal income)
    {
        return income < TaxConsts.FlatValueThreshold ? 
            Math.Round(income * (TaxConsts.FlatValuePercentage/100),2) : 
            Math.Round(TaxConsts.FlatValueFixedAmount,2);
    }   
    
    /// <summary>
    /// Calculate the Flat Rate tax for annual income
    /// </summary>
    /// <param name="income">Annual Income</param>
    /// <returns>Tax amount</returns>
    public virtual decimal CalculateFlatRateTax(decimal income)
    {
        return Math.Round(income * (TaxConsts.FlatRatePercentage/100),2);
    }
}