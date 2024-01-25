﻿using Microsoft.Extensions.DependencyInjection;

namespace PayspaceTax.Mapping;

public static class DependencyInjection
{
    public static void AddMapping(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(DependencyInjection).Assembly);
    }
}