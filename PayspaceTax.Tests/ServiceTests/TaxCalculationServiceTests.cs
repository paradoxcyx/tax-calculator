using Moq;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Application.Services;
using PayspaceTax.Domain.Entities;
using PayspaceTax.Domain.Enums;
using PayspaceTax.Domain.Exceptions;
using PayspaceTax.Domain.Helpers;

namespace PayspaceTax.Tests.ServiceTests;

    [TestFixture]
    public class TaxCalculationServiceTests
    {
        [Test]
        public async Task CalculateTaxAsync_WithValidPostalCode_ShouldReturnCalculateTaxDto()
        {
            // Arrange
            var postalCode = "7441";
            var annualIncome = 5000m;
            
            var calculationTypes = new List<PostalCodeTaxCalculationType>
            {
                new()
                {
                    PostalCode = postalCode,
                    TaxCalculationType = (int)TaxCalculationTypeEnum.Progressive
                }

            }.AsQueryable();
            
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = postalCode,
                AnnualIncome = annualIncome
            };

            var progressiveTaxBrackets = new List<ProgressiveTaxBracket>
            {
                new()
                {
                    From = 0,
                    To = 8350,
                    RatePercentage = 10 // For example
                }

            }.AsQueryable();
            
            var progressiveTaxBracketRepositoryMock = new Mock<IRepository<ProgressiveTaxBracket>>();
            progressiveTaxBracketRepositoryMock.Setup(repo => repo.GetAll())
                .Returns(progressiveTaxBrackets);

            var postalCodeTaxCalculationTypeRepositoryMock = new Mock<IRepository<PostalCodeTaxCalculationType>>();
            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetAll())
                .Returns(calculationTypes);

            var taxCalculationHelper = new TaxCalculationHelper();
            var taxCalculationService = new TaxCalculationService(
                progressiveTaxBracketRepositoryMock.Object,
                postalCodeTaxCalculationTypeRepositoryMock.Object,
                taxCalculationHelper);

            // Act
            var result = await taxCalculationService.CalculateTaxAsync(calculateTaxDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Tax, Is.EqualTo(500m)); // Assuming the tax calculation is correct
        }

        [Test]
        public void CalculateTaxAsync_WithInvalidPostalCode_ShouldThrowPaySpaceTaxException()
        {
            // Arrange
            var postalCode = "invalid_code";
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = postalCode,
                AnnualIncome = 50000m
            };
            
            var calculationTypes = new List<PostalCodeTaxCalculationType>
            {
                new()
                {
                    PostalCode = "7441",
                    TaxCalculationType = (int)TaxCalculationTypeEnum.Progressive
                }

            }.AsQueryable();
            

            var postalCodeTaxCalculationTypeRepositoryMock = new Mock<IRepository<PostalCodeTaxCalculationType>>();
            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetAll())
                .Returns(calculationTypes);

            var taxCalculationHelper = new TaxCalculationHelper();
            var taxCalculationService = new TaxCalculationService(
            It.IsAny<IRepository<ProgressiveTaxBracket>>(),
                postalCodeTaxCalculationTypeRepositoryMock.Object,
                taxCalculationHelper);

            // Act + Assert
            Assert.ThrowsAsync<PaySpaceTaxException>(() => taxCalculationService.CalculateTaxAsync(calculateTaxDto));
        }
    
    }