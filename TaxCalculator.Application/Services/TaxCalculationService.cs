﻿using Microsoft.EntityFrameworkCore;
using TaxCalculator.Application.DTOs;
using TaxCalculator.Application.Interfaces.Repositories;
using TaxCalculator.Application.Interfaces.Services;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Enums;
using TaxCalculator.Domain.Exceptions;
using TaxCalculator.Domain.Helpers;

namespace TaxCalculator.Application.Services;

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

    /// <summary>
    /// Retrieve the tax calculation type for postal code
    /// </summary>
    /// <param name="postalCode">Postal Code</param>
    /// <returns>The calculation type</returns>
    /// <exception cref="TaxCalculatorException">if the postal code does not exist</exception>
    private async Task<TaxCalculationTypeEnum> GetTaxCalculationTypeAsync(string postalCode)
    {
        var calculationType =
            await postalCodeTaxCalculationTypeRepository.GetFirstByAsync(x => x.PostalCode.Equals(postalCode));

        if (calculationType == null)
            throw new TaxCalculatorException($"{postalCode} is an invalid Postal Code");

        return (TaxCalculationTypeEnum)calculationType.TaxCalculationType;
    }

    /// <summary>
    /// Calculate the tax based on the type
    /// </summary>
    /// <param name="annualIncome">Annual income</param>
    /// <param name="calculationType">The calculation type</param>
    /// <returns></returns>
    /// <exception cref="TaxCalculatorException">if the calculation is not found</exception>
    private async Task<decimal> CalculateTaxBasedOnTypeAsync(decimal annualIncome, TaxCalculationTypeEnum calculationType)
    {
        return calculationType switch
        {
            TaxCalculationTypeEnum.Progressive => await CalculateProgressiveTaxAsync(annualIncome),
            TaxCalculationTypeEnum.FlatRate => taxCalculationHelper.CalculateFlatRateTax(annualIncome),
            TaxCalculationTypeEnum.FlatValue => taxCalculationHelper.CalculateFlatValueTax(annualIncome),
            _ => throw new TaxCalculatorException("Tax Calculation Type not found")
        };
    }

    /// <summary>
    /// Calculate tax following the progressive tax brackets
    /// </summary>
    /// <param name="annualIncome">Annual Income</param>
    /// <returns>The progressive tax amount</returns>
    /// <exception cref="TaxCalculatorException">If the tax bracket is not found</exception>
    private async Task<decimal> CalculateProgressiveTaxAsync(decimal annualIncome)
    {
        var taxBracket = await progressiveTaxBracketRepository.GetFirstByAsync(x =>
            (x.To.HasValue && x.From <= annualIncome && x.To >= annualIncome) ||
            (!x.To.HasValue && x.From <= annualIncome));

        if (taxBracket == null)
            throw new TaxCalculatorException("Progressive Tax Bracket does not exist");

        return taxCalculationHelper.CalculateProgressiveTax(annualIncome, taxBracket.RatePercentage);
    }
}
