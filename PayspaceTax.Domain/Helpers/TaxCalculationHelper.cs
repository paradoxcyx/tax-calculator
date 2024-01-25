using PayspaceTax.Domain.Consts;

namespace PayspaceTax.Domain.Helpers;

public class TaxCalculationHelper
{
    public decimal CalculateProgressiveTax(decimal income, decimal ratePercentage)
    {
        return Math.Round(income * (ratePercentage/100),2);
    }

    public decimal CalculateFlatValueTax(decimal income)
    {
        
        if (income < TaxConsts.FlatValueThreshold)
        {
            return Math.Round(income * (TaxConsts.FlatValuePercentage/100),2);
        }

        return Math.Round(TaxConsts.FlatValueFixedAmount,2);
    }

    public decimal CalculateFlatRateTax(decimal income)
    {
        return Math.Round(income * (TaxConsts.FlatRatePercentage/100),2);
    }
}