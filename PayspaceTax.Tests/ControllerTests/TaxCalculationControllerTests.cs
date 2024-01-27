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
        private Mock<ITaxCalculationService>? _mockTaxCalculationService;
        private Mock<ITaxCalculationHistoryService>? _mockTaxCalculationHistoryService;
        private Mock<IMapper>? _mockMapper;
        
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _mockTaxCalculationService = new Mock<ITaxCalculationService>();
            _mockTaxCalculationHistoryService = new Mock<ITaxCalculationHistoryService>();
            _mockMapper = new Mock<IMapper>();
        }
        
        [Test]
        public async Task CalculateTax_ValidInput_ReturnsOk()
        {
            // instantiating a new mock tax calculation controller
            var controller = new TaxCalculationController(
                _mockTaxCalculationService!.Object,
                _mockTaxCalculationHistoryService!.Object,
                _mockMapper!.Object);

            // mocking input that will essentially be retrieved from front end
            var input = new CalculateTaxInput { AnnualIncome = 5000, PostalCode = "7441" };

            // mocking the output of the mapper. input > dto
            var calculationDto = new CalculateTaxDto {AnnualIncome = 5000, PostalCode = "7441"};
            
            _mockMapper.Setup(m => m.Map<CalculateTaxDto>(input)).Returns(calculationDto);

            var taxCalculationResult = new CalculateTaxDto {AnnualIncome = 5000, PostalCode = "7441", Tax = 500}; // Provide valid tax calculation result for testing
            _mockTaxCalculationService.Setup(s => s.CalculateTaxAsync(calculationDto)).ReturnsAsync(taxCalculationResult);

            var historyDto = new TaxCalculationHistoryDto {PostalCode = "7441", Tax = 500, AnnualIncome = 5000, CalculatedDate = DateTime.Now }; // Provide valid history DTO for testing
            _mockMapper.Setup(m => m.Map<TaxCalculationHistoryDto>(taxCalculationResult)).Returns(historyDto);

            // Act
            var result = await controller.CalculateTax(input) as OkObjectResult;

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            var responseModel = result?.Value as GenericResponseModel<CalculateTaxDto>;
            Assert.That(responseModel, Is.Not.Null);
            
            Assert.Multiple(() =>
            {
                Assert.That(responseModel is { Success: true }, Is.True);
                Assert.That(responseModel?.Data, Is.SameAs(calculationDto));
            });

            // Verify that services were called with correct parameters
            _mockMapper.Verify(m => m.Map<CalculateTaxDto>(input), Times.Once);
            _mockTaxCalculationService.Verify(s => s.CalculateTaxAsync(calculationDto), Times.Once);
            _mockMapper.Verify(m => m.Map<TaxCalculationHistoryDto>(taxCalculationResult), Times.Once);
            _mockTaxCalculationHistoryService.Verify(s => s.AddAsync(historyDto), Times.Once);
        }

        [Test]
        public async Task History_ValidInput_ReturnsOk()
        {
            // instantiating a new mock tax calculation controller
            var controller = new TaxCalculationController(
                _mockTaxCalculationService!.Object,
                _mockTaxCalculationHistoryService!.Object,
                _mockMapper!.Object);

            var taxCalculationHistoriesResult = new List<TaxCalculationHistoryDto>
            {
                new()
                {
                    AnnualIncome = 5000m,
                    PostalCode = "7441",
                    CalculatedDate = DateTime.Now,
                    Tax = 500m
                },
                new()
                {
                    AnnualIncome = 201342.10m,
                    PostalCode = "A100",
                    CalculatedDate = DateTime.Now,
                    Tax = 10000m,
                }
            };

            _mockTaxCalculationHistoryService.Setup(s => s.GetTaxCalculationHistoryAsync())
                .ReturnsAsync(taxCalculationHistoriesResult);
            
            var result = await controller.GetHistory() as OkObjectResult;
            
            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result?.StatusCode, Is.EqualTo(200));
            var responseModel = result?.Value as GenericResponseModel<List<TaxCalculationHistoryDto>>;
            Assert.That(responseModel, Is.Not.Null);
            
            Assert.Multiple(() =>
            {
                Assert.That(responseModel is { Success: true }, Is.True);
                Assert.That(responseModel?.Data, Is.SameAs(taxCalculationHistoriesResult));
            });
            
            _mockTaxCalculationHistoryService.Verify(s => s.GetTaxCalculationHistoryAsync(), Times.Once);
        }
}