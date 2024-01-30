using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Domain.Entities;

public class TaxCalculationHistory
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string PostalCode { get; set; }
    
    [Required]
    public decimal AnnualIncome { get; set; }
    
    [Required]
    public decimal Tax { get; set; }
    
    [Required]
    public DateTime CalculatedDate { get; set; }
}