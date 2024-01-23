using Microsoft.EntityFrameworkCore;
using PayspaceTax.Infrastructure.Entities;

namespace PayspaceTax.Infrastructure.Database;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    
    public DbSet<ProgressiveTaxBracket> ProgressiveTaxBrackets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProgressiveTaxBracket>()
            .ToTable("ProgressiveTaxBrackets")
            .HasNoKey();
    }
}