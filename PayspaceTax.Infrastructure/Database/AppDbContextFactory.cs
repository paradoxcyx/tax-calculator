using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PayspaceTax.Infrastructure.Database;

internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext>();
        const string connString = "Server=localhost;Database=PayspaceTax;Trusted_Connection=True;Encrypt=False";
        dbContextBuilder.UseSqlServer(connString);
        return new AppDbContext(dbContextBuilder.Options);
    }
}