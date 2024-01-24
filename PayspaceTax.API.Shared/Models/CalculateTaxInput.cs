using System.ComponentModel.DataAnnotations;

namespace PayspaceTax.API.Shared.Models;

public class CalculateTaxInput
{
    [Required]
    public string PostalCode { get; set; }
    
    [Required]
    public decimal AnnualIncome { get; set; }
}