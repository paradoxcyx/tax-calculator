using System.ComponentModel.DataAnnotations;

namespace PayspaceTax.Domain.Entities;

public class ProgressiveTaxBracket
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public decimal From { get; set; }
    
    public decimal? To { get; set; }
    
    [Required]
    public decimal RatePercentage { get; set; }
}