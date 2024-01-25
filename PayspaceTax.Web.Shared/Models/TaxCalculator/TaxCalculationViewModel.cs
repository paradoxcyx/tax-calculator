using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace PayspaceTax.Web.Shared.Models.TaxCalculator;

public class TaxCalculationViewModel
{
    [DisplayName("Postal Code")]
    [Required(ErrorMessage = "Postal Code is required")]
    public string PostalCode { get; set; }
    
    [DisplayName("Annual Income")]
    [Required(ErrorMessage = "Annual Income is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Minimum Annual Income of 0 required")]
    public decimal AnnualIncome { get; set; }
    
    public decimal? Tax { get; set; }
}