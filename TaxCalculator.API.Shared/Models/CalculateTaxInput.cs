using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.API.Shared.Models;

public class CalculateTaxInput
{
    [Required]
    public string PostalCode { get; set; }
    
    [Required]
    public decimal AnnualIncome { get; set; }
}