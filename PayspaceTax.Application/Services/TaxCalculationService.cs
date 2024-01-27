using Microsoft.EntityFrameworkCore;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Application.Interfaces.Services;
using PayspaceTax.Domain.Entities;
using PayspaceTax.Domain.Enums;
using PayspaceTax.Domain.Exceptions;
using PayspaceTax.Domain.Helpers;

namespace PayspaceTax.Application.Services;

public class TaxCalculationService(
    IRepository<ProgressiveTaxBracket> progressiveTaxBracketRepository,
    IRepository<PostalCodeTaxCalculationType> postalCodeTaxCalculationTypeRepository,
    TaxCalculationHelper taxCalculationHelper)
    : ITaxCalculationService
{
    public async Task<CalculateTaxDto> CalculateTaxAsync(CalculateTaxDto calculateTax)
    {
        var calculationType =
            await postalCodeTaxCalculationTypeRepository
                .GetAll()
                .FirstOrDefaultAsync(x => x.PostalCode.Equals(calculateTax.PostalCode));
        
        if (calculationType == null)
            throw new PaySpaceTaxException($"{calculateTax.PostalCode} is an invalid Postal Code");

        calculateTax.Tax = await CalculateTaxBasedOnTypeAsync(calculateTax.AnnualIncome, (TaxCalculationTypeEnum)calculationType.TaxCalculationType);
        
        return calculateTax;
    }

    private async Task<decimal> CalculateTaxBasedOnTypeAsync(decimal annualIncome, TaxCalculationTypeEnum calculationType)
    {
        switch (calculationType)
        {
            case TaxCalculationTypeEnum.Progressive:
                
                var taxBracket = await progressiveTaxBracketRepository.GetAll().FirstOrDefaultAsync(x =>
                    (x.To.HasValue && x.From <= annualIncome && x.To >= annualIncome) ||
                    (!x.To.HasValue && x.From <= annualIncome));
                
                if (taxBracket == null)
                    throw new PaySpaceTaxException("Progressive Tax Bracket does not exist");
                
                return taxCalculationHelper.CalculateProgressiveTax(annualIncome, taxBracket.RatePercentage);

            case TaxCalculationTypeEnum.FlatRate:
                return taxCalculationHelper.CalculateFlatRateTax(annualIncome);

            case TaxCalculationTypeEnum.FlatValue:
                return taxCalculationHelper.CalculateFlatValueTax(annualIncome);

            default:
                throw new PaySpaceTaxException("Tax Calculation Type not found");
        }
    }
}