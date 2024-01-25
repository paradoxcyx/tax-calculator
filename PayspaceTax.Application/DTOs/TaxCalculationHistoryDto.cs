namespace PayspaceTax.Application.DTOs;

public class TaxCalculationHistoryDto
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal Tax { get; set; }
    public DateTime CalculatedDate { get; set; }
}