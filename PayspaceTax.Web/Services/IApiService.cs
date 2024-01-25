using PayspaceTax.Web.Shared.Models.TaxCalculator;

namespace PayspaceTax.Web.Services;

public interface IApiService
{
    public Task<TaxCalculationViewModel?> CalculateTax(TaxCalculationViewModel model);
}