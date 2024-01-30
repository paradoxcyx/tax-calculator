using Microsoft.EntityFrameworkCore;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public virtual DbSet<ProgressiveTaxBracket> ProgressiveTaxBrackets { get; set; }
    
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