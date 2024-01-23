using System.ComponentModel.DataAnnotations;

namespace PayspaceTax.Infrastructure.Entities;

public class ProgressiveTaxBracket
{
    [Required]
    public decimal From { get; set; }
    
    public decimal? To { get; set; }
    
    [Required]
    public decimal RatePercentage { get; set; }
}