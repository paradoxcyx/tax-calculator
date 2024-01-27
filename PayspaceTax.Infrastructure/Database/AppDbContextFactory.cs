using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PayspaceTax.Infrastructure.Database;

internal class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    /// <summary>
    /// Creates a database context in design time to facilitate migrations and database updates
    /// </summary>
    /// <param name="args">Any command line arguments</param>
    /// <returns></returns>
    /// <remarks>NOTE: Contains a statically defined connection string to enable users to facilitate migrations and database updates directly in the Infrastructure project</remarks>
    public AppDbContext CreateDbContext(string[] args)
    {
        var dbContextBuilder = new DbContextOptionsBuilder<AppDbContext>();
        const string connString = "Server=localhost;Database=PayspaceTax;Trusted_Connection=True;Encrypt=False";
        dbContextBuilder.UseSqlServer(connString);
        return new AppDbContext(dbContextBuilder.Options);
    }
}