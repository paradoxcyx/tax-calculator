using System.ComponentModel.DataAnnotations;

namespace TaxCalculator.Domain.Entities;

public class PostalCodeTaxCalculationType
{
    [Key]
    public int Id { get; set; }
    
    public string PostalCode { get; set; }
    
    public int TaxCalculationType { get; set; } 
}