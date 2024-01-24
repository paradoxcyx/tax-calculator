using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Application.Interfaces.Services;
using PayspaceTax.Domain.Consts;
using PayspaceTax.Domain.Enums;
using PayspaceTax.Domain.Exceptions;
using PayspaceTax.Domain.Helpers;

namespace PayspaceTax.Application.Services;

public class TaxCalculationService(IProgressiveTaxBracketRepository progressiveTaxBracketRepository, IPostalCodeTaxCalculationTypeRepository postalCodeTaxCalculationTypeRepository, ITaxCalculationHelper taxCalculationHelper)
    : ITaxCalculationService
{
    public async Task<CalculateTaxDto> CalculateTaxAsync(CalculateTaxDto calculateTax)
    {
        var calculationType =
            await postalCodeTaxCalculationTypeRepository.GetTaxCalculationTypeByPostalCode(calculateTax.PostalCode);

        if (calculationType == null)
            throw new TaxCalculationException("Calculation Type for postal code does not exist");

        switch (calculationType.TaxCalculationType)
        {
            case (int)TaxCalculationTypeEnum.Progressive:
                var taxBracket =
                    await progressiveTaxBracketRepository.GetProgressiveTaxBracketByIncomeAsync(calculateTax.AnnualIncome);

                if (taxBracket == null)
                    throw new TaxCalculationException("Tax bracket does not exist");

                calculateTax.Tax =
                    taxCalculationHelper.CalculateProgressiveTax(calculateTax.AnnualIncome, taxBracket.RatePercentage);
                break;
            case (int)TaxCalculationTypeEnum.FlatRate:
                calculateTax.Tax = taxCalculationHelper.CalculateFlatRateTax(calculateTax.AnnualIncome);
                break;
            
            case (int)TaxCalculationTypeEnum.FlatValue:
                calculateTax.Tax = taxCalculationHelper.CalculateFlatValueTax(calculateTax.AnnualIncome);
                break;
        }

        return calculateTax;
    }
}