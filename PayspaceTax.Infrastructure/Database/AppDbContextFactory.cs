using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace PayspaceTax.Infrastructure.Database
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var environment = args.Length > 0 ? args[0] : "Development";

            // Get the directory where the assembly is located
            var assemblyLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            // Build the configuration from the appsettings.json file in PayspaceTax.API project
            var configuration = new ConfigurationBuilder()
                .SetBasePath(assemblyLocation)
                .AddJsonFile(Path.Combine(assemblyLocation, "../PayspaceTax.API/appsettings.json"), optional: false, reloadOnChange: true)
                .AddJsonFile(Path.Combine(assemblyLocation, "../PayspaceTax.API/appsettings." + environment + ".json"), optional: true, reloadOnChange: true)
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new AppDbContext(optionsBuilder.Options);
        }
    }
}