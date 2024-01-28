using PayspaceTax.Domain.Consts;

namespace PayspaceTax.Domain.Helpers;

public class TaxCalculationHelper
{
    public virtual decimal CalculateProgressiveTax(decimal income, decimal ratePercentage)
    {
        return Math.Round(income * (ratePercentage/100),2);
    }
    
    public virtual decimal CalculateFlatValueTax(decimal income)
    {
        return income < TaxConsts.FlatValueThreshold ? 
            Math.Round(income * (TaxConsts.FlatValuePercentage/100),2) : 
            Math.Round(TaxConsts.FlatValueFixedAmount,2);
    }   
    
    public virtual decimal CalculateFlatRateTax(decimal income)
    {
        return Math.Round(income * (TaxConsts.FlatRatePercentage/100),2);
    }
}