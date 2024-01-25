using Microsoft.EntityFrameworkCore;
using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<ProgressiveTaxBracket> ProgressiveTaxBrackets { get; set; }
    
    public DbSet<PostalCodeTaxCalculationType> PostalCodeTaxCalculationTypes { get; set; }
    
    public DbSet<TaxCalculationHistory> TaxCalculationHistories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgressiveTaxBracket>()
            .ToTable("ProgressiveTaxBrackets")
            .HasKey(x => x.Id);
        
        modelBuilder.Entity<PostalCodeTaxCalculationType>()
            .ToTable("PostalCodeTaxCalculationTypes")
            .HasKey(x => x.Id);

        modelBuilder.Entity<TaxCalculationHistory>()
            .ToTable("TaxCalculationHistories")
            .HasKey(x => x.Id);

        modelBuilder.Entity<TaxCalculationHistory>()
            .Property(t => t.CalculatedDate)
            .HasDefaultValueSql("GETDATE()");
    }
}