using PayspaceTax.Application.DTOs;

namespace PayspaceTax.Application.Interfaces.Services;

public interface ITaxCalculationService
{
    public Task<CalculateTaxDto> CalculateTaxAsync(CalculateTaxDto calculateTax);
}