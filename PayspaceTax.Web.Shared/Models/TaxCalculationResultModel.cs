namespace PayspaceTax.Web.Shared.Models;

public class TaxCalculationResultModel
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal TaxAmount { get; set; }
}