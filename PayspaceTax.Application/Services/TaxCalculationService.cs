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
        var calculationType = await GetTaxCalculationTypeAsync(calculateTax.PostalCode);

        calculateTax.Tax = await CalculateTaxBasedOnTypeAsync(calculateTax.AnnualIncome, calculationType);

        return calculateTax;
    }

    private async Task<TaxCalculationTypeEnum> GetTaxCalculationTypeAsync(string postalCode)
    {
        var calculationType =
            await postalCodeTaxCalculationTypeRepository.GetFirstByAsync(x => x.PostalCode.Equals(postalCode));

        if (calculationType == null)
            throw new PaySpaceTaxException($"{postalCode} is an invalid Postal Code");

        return (TaxCalculationTypeEnum)calculationType.TaxCalculationType;
    }

    private async Task<decimal> CalculateTaxBasedOnTypeAsync(decimal annualIncome, TaxCalculationTypeEnum calculationType)
    {
        return calculationType switch
        {
            TaxCalculationTypeEnum.Progressive => await CalculateProgressiveTaxAsync(annualIncome),
            TaxCalculationTypeEnum.FlatRate => taxCalculationHelper.CalculateFlatRateTax(annualIncome),
            TaxCalculationTypeEnum.FlatValue => taxCalculationHelper.CalculateFlatValueTax(annualIncome),
            _ => throw new PaySpaceTaxException("Tax Calculation Type not found")
        };
    }

    private async Task<decimal> CalculateProgressiveTaxAsync(decimal annualIncome)
    {
        var taxBracket = await progressiveTaxBracketRepository.GetFirstByAsync(x =>
            (x.To.HasValue && x.From <= annualIncome && x.To >= annualIncome) ||
            (!x.To.HasValue && x.From <= annualIncome));

        if (taxBracket == null)
            throw new PaySpaceTaxException("Progressive Tax Bracket does not exist");

        return taxCalculationHelper.CalculateProgressiveTax(annualIncome, taxBracket.RatePercentage);
    }
}
