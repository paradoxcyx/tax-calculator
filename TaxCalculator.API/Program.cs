using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TaxCalculator.API.Middlewares;
using TaxCalculator.Application;
using TaxCalculator.Domain;
using TaxCalculator.Infrastructure;
using TaxCalculator.Mapping;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options =>
{
    // Registering the global exception handler attribute using a factory method
    options.Filters.Add(typeof(GlobalExceptionHandlerAttributeFactory));
}).ConfigureApiBehaviorOptions(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        //Using a custom validation handler for modal binding errors e.g. when a value is required but not populated
        var problems = new EndpointValidationHandler<object>(context);
        return new BadRequestObjectResult(problems.Value);
    };
});
    
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMapping();
builder.Services.AddDomain();
builder.Services.AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty);
builder.Services.AddApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

// Factory method for creating instances of GlobalExceptionHandlerAttribute with injected dependencies
public class GlobalExceptionHandlerAttributeFactory : IFilterFactory
{
    public IFilterMetadata CreateInstance(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<GlobalExceptionHandlerAttribute>>();
        return new GlobalExceptionHandlerAttribute(logger);
    }

    public bool IsReusable => false;
}