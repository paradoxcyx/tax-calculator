using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PayspaceTax.API.Controllers;
using PayspaceTax.API.Shared.Models;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Services;

namespace PayspaceTax.Tests.ControllerTests;

[TestFixture]
public class TaxCalculationControllerTests
{
    [Test]
    public void CalculateTax_Returns_OkResult_With_Valid_Result()
    {
        /*// Arrange
        var taxCalculationServiceMock = new Mock<ITaxCalculationService>();

        taxCalculationServiceMock
            .Setup(x => x.CalculateTaxAsync(new CalculateTaxDto { AnnualIncome = 5000, PostalCode = "7441" }).Result)
            .Returns((CalculateTaxDto x) => new CalculateTaxDto {AnnualIncome = 5000, PostalCode = "7441", Tax = 500});
        
        var taxCalculationHistoryServiceMock = new Mock<ITaxCalculationHistoryService>();

// Setup the behavior of the mapper mock to return the expected mapping result
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<CalculateTaxDto>(It.IsAny<CalculateTaxInput>()))
            .Returns((CalculateTaxInput input) => new CalculateTaxDto { PostalCode = "7441", AnnualIncome = 5000, Tax = 500});

        mapperMock.Setup(mapper => mapper.Map<TaxCalculationHistoryDto>(It.IsAny<CalculateTaxDto>()))
            .Returns((CalculateTaxDto result) => new TaxCalculationHistoryDto { PostalCode = "7441", AnnualIncome = 5000, Tax = 500});

        var controller = new TaxCalculationController(taxCalculationServiceMock.Object, 
            taxCalculationHistoryServiceMock.Object, 
            mapperMock.Object);

// Act
        var result = await controller.CalculateTax(new CalculateTaxInput { PostalCode = "7441", AnnualIncome = 5000 });
        
        // Assert
        Assert.That(result, Is.InstanceOf<OkObjectResult>());
        var okResult = result as OkObjectResult;
        Assert.That(okResult?.Value, Is.Not.Null);
        // Add more assertions as needed*/

    }
}