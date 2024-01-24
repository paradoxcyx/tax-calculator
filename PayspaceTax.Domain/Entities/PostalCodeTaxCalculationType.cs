using System.ComponentModel.DataAnnotations;

namespace PayspaceTax.Domain.Entities;

public class PostalCodeTaxCalculationType
{
    [Key]
    public int Id { get; set; }
    
    public string PostalCode { get; set; }
    
    public int TaxCalculationType { get; set; } 
}