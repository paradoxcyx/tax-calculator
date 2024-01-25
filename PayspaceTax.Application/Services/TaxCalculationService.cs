using AutoMapper;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Application.Interfaces.Services;
using PayspaceTax.Domain.Entities;
using PayspaceTax.Domain.Enums;
using PayspaceTax.Domain.Exceptions;
using PayspaceTax.Domain.Helpers;

namespace PayspaceTax.Application.Services;

public class TaxCalculationService(
    IProgressiveTaxBracketRepository progressiveTaxBracketRepository, 
    IPostalCodeTaxCalculationTypeRepository postalCodeTaxCalculationTypeRepository, 
    ITaxCalculationHistoryRepository taxCalculationHistoryRepository,
    IMapper mapper,
    TaxCalculationHelper taxCalculationHelper)
    : ITaxCalculationService
{
    public async Task<CalculateTaxDto> CalculateTaxAsync(CalculateTaxDto calculateTax)
    {
        try
        {
            var calculationType = await postalCodeTaxCalculationTypeRepository.GetTaxCalculationTypeByPostalCode(calculateTax.PostalCode);

            if (calculationType == null)
                throw new TaxCalculationException($"Calculation Type for postal code {calculateTax.PostalCode} does not exist");

            calculateTax.Tax = await CalculateTaxBasedOnTypeAsync(calculateTax.AnnualIncome, (TaxCalculationTypeEnum)calculationType.TaxCalculationType);

            var history = mapper.Map<TaxCalculationHistory>(calculateTax);
            await taxCalculationHistoryRepository.AddAsync(history);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it according to your application's needs
            throw new TaxCalculationException($"Error calculating tax: {ex.Message}");
        }

        return calculateTax;
    }

    private async Task<decimal> CalculateTaxBasedOnTypeAsync(decimal annualIncome, TaxCalculationTypeEnum calculationType)
    {
        switch (calculationType)
        {
            case TaxCalculationTypeEnum.Progressive:
                var taxBracket = await progressiveTaxBracketRepository.GetProgressiveTaxBracketByIncomeAsync(annualIncome);
                if (taxBracket == null)
                    throw new TaxCalculationException("Tax bracket does not exist");
                
                return taxCalculationHelper.CalculateProgressiveTax(annualIncome, taxBracket.RatePercentage);

            case TaxCalculationTypeEnum.FlatRate:
                return taxCalculationHelper.CalculateFlatRateTax(annualIncome);

            case TaxCalculationTypeEnum.FlatValue:
                return taxCalculationHelper.CalculateFlatValueTax(annualIncome);

            default:
                throw new TaxCalculationException("Invalid tax calculation type");
        }
    }
}