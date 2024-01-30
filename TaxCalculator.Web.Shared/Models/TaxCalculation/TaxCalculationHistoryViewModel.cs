namespace TaxCalculator.Web.Shared.Models.TaxCalculation;

public class TaxCalculationHistoryViewModel
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal Tax { get; set; }
    public DateTime CalculatedDate { get; set; }
}