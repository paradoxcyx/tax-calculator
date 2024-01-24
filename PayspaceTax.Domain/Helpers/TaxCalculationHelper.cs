using PayspaceTax.Domain.Consts;

namespace PayspaceTax.Domain.Helpers;

public class TaxCalculationHelper : ITaxCalculationHelper
{
    public decimal CalculateProgressiveTax(decimal income, decimal ratePercentage)
    {
        return income * (ratePercentage/1000);
    }

    public decimal CalculateFlatValueTax(decimal income)
    {
        if (income < TaxConsts.FlatValueThreshold)
        {
            return income * (TaxConsts.FlatValuePercentage/100);
        }

        return TaxConsts.FlatValueFixedAmount;
    }

    public decimal CalculateFlatRateTax(decimal income)
    {
        return income * (TaxConsts.FlatRatePercentage/100);
    }
}