﻿using Microsoft.Extensions.DependencyInjection;
using PayspaceTax.Infrastructure.Database;
using PayspaceTax.Infrastructure.Entities;

namespace PayspaceTax.Infrastructure.Util;

public static class DbSeeder
{
    public static void SeedData(IServiceCollection services)
    {
        using var serviceProvider = services.BuildServiceProvider();
        var dbContext = serviceProvider.GetRequiredService<AppDbContext>();

        // Check if the database has been created
        dbContext.Database.EnsureCreated();

        // Seed default values
        if (dbContext.ProgressiveTaxBrackets.Any()) return;
        
        dbContext.ProgressiveTaxBrackets.AddRange(
            new ProgressiveTaxBracket { From = 0, To = 8350, RatePercentage = 10 },
            new ProgressiveTaxBracket { From = 8351, To = 33950, RatePercentage = 15 },
            new ProgressiveTaxBracket { From = 33951, To = 82250, RatePercentage = 25 },
            new ProgressiveTaxBracket { From = 82251, To = 171550, RatePercentage = 28 },
            new ProgressiveTaxBracket { From = 171551, To = 372950, RatePercentage = 33 },
            new ProgressiveTaxBracket { From = 372951, To = null, RatePercentage = 35 }
        );

        dbContext.SaveChanges();
    }
}