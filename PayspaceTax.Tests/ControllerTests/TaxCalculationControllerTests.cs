using Moq;
using PayspaceTax.API.Controllers;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Services;

namespace PayspaceTax.Tests.ControllerTests;

[TestFixture]
public class TaxCalculationControllerTests
{
    [Test]
    public void CalculateTax_Returns_OkResult_With_Valid_Result()
    {
        /*var taxCalculationServiceMock = new Mock<ITaxCalculationService>();
        taxCalculationServiceMock.Setup(service => service.CalculateTaxAsync(It.IsAny<CalculateTaxDto>()))
            .ReturnsAsync(new CalculateTaxDto { PostalCode = "", AnnualIncome = 1, Tax = 1 });

        var controller = new TaxCalculationController(taxCalculationServiceMock.Object, null, null);
        */
        
    }
}