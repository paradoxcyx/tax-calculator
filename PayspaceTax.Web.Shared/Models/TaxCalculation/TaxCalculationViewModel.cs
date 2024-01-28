using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PayspaceTax.Web.Shared.Models.TaxCalculation;

public class TaxCalculationViewModel
{
    public TaxCalculationDataViewModel? TaxData { get; set; }
    public string? ErrorMessage { get; set; }
}

public class TaxCalculationDataViewModel {
    [DisplayName("Postal Code")]
    [Required(ErrorMessage = "Postal Code is required")]
    public string PostalCode { get; set; }
    
    [DisplayName("Annual Income")]
    [Required(ErrorMessage = "Annual Income is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Minimum Annual Income of 1 required")]
    public decimal AnnualIncome { get; set; }
    
    public decimal? Tax { get; set; }
}