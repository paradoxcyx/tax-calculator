namespace PayspaceTax.Application.DTOs;

public class CalculateTaxDto
{
    public string PostalCode { get; set; }
    public decimal AnnualIncome { get; set; }
    public decimal? Tax { get; set; }
}