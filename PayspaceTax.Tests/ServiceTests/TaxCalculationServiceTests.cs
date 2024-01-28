using System.Linq.Expressions;
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
        private Mock<IRepository<ProgressiveTaxBracket>> progressiveTaxBracketRepositoryMock;
        private Mock<IRepository<PostalCodeTaxCalculationType>> postalCodeTaxCalculationTypeRepositoryMock;
        private Mock<TaxCalculationHelper> taxCalculationHelperMock; 
        private TaxCalculationService taxCalculationService;

        [SetUp]
        public void Setup()
        {
            progressiveTaxBracketRepositoryMock = new Mock<IRepository<ProgressiveTaxBracket>>();
            postalCodeTaxCalculationTypeRepositoryMock = new Mock<IRepository<PostalCodeTaxCalculationType>>();
            taxCalculationHelperMock = new Mock<TaxCalculationHelper>();

            taxCalculationService = new TaxCalculationService(
                progressiveTaxBracketRepositoryMock.Object,
                postalCodeTaxCalculationTypeRepositoryMock.Object,
                taxCalculationHelperMock.Object);
        }

        [Test]
        public async Task CalculateTaxAsync_ValidPostalCode_ReturnsCalculateTaxDtoWithTax()
        {
            const decimal expectedTaxAmount = 500;
            
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = "7441",
                AnnualIncome = 5000
            };

            var postalCodeTaxCalculationType = new PostalCodeTaxCalculationType
            {
                PostalCode = "7441",
                TaxCalculationType = (int)TaxCalculationTypeEnum.Progressive
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync(postalCodeTaxCalculationType);

            var taxBracket = new ProgressiveTaxBracket
            {
                From = 0,
                To = 8350,
                RatePercentage = 10
            };

            progressiveTaxBracketRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<ProgressiveTaxBracket, bool>>>()))
                .ReturnsAsync(taxBracket);

            taxCalculationHelperMock.Setup(helper => helper.CalculateProgressiveTax(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(expectedTaxAmount);

            // Act
            var result = await taxCalculationService.CalculateTaxAsync(calculateTaxDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Tax, Is.EqualTo(expectedTaxAmount)); // Expected tax: 50000 * 10% = 5000
        }

        [Test]
        public Task CalculateTaxAsync_InvalidPostalCode_ThrowsException()
        {
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = "InvalidPostalCode", // Assuming this postal code is invalid
                AnnualIncome = 50000
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync((PostalCodeTaxCalculationType)null); // Simulating not finding a tax calculation type for the given postal code

            // Act and Assert
            var exception = Assert.ThrowsAsync<PaySpaceTaxException>(() => taxCalculationService.CalculateTaxAsync(calculateTaxDto));
            Assert.That(exception?.Message, Is.EqualTo("InvalidPostalCode is an invalid Postal Code"));
            return Task.CompletedTask;  
        }

        [Test]
        public Task CalculateTaxAsync_ProgressiveTaxBracketNotFound_ThrowsException()
        {
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = "ValidPostalCode",
                AnnualIncome = 50000
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync(new PostalCodeTaxCalculationType { PostalCode = "ValidPostalCode", TaxCalculationType = (int)TaxCalculationTypeEnum.Progressive});

            progressiveTaxBracketRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<ProgressiveTaxBracket, bool>>>()))
                .ReturnsAsync((ProgressiveTaxBracket)null); // Simulating not finding a progressive tax bracket

            // Act and Assert
            var exception = Assert.ThrowsAsync<PaySpaceTaxException>(() => taxCalculationService.CalculateTaxAsync(calculateTaxDto));
            Assert.That(exception?.Message, Is.EqualTo("Progressive Tax Bracket does not exist"));
            return Task.CompletedTask;
        }
        
        [Test]
        public async Task CalculateTaxAsync_FlatRateType_ReturnsCalculateTaxDtoWithTax()
        {
            const decimal expectedTaxAmount = 8750;
            
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = "7000",
                AnnualIncome = 50000
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync(new PostalCodeTaxCalculationType { PostalCode = "7000", TaxCalculationType = (int)TaxCalculationTypeEnum.FlatRate });

            taxCalculationHelperMock.Setup(helper => helper.CalculateFlatRateTax(It.IsAny<decimal>()))
                .Returns(expectedTaxAmount);

            // Act
            var result = await taxCalculationService.CalculateTaxAsync(calculateTaxDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Tax, Is.EqualTo(expectedTaxAmount));
        }

        [Test]
        public async Task CalculateTaxAsync_FlatValueType_ReturnsCalculateTaxDtoWithTax()
        {
            const decimal expectedTaxAmount = 2500;
            
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = "A100",
                AnnualIncome = 50000
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync(new PostalCodeTaxCalculationType { PostalCode = "A100", TaxCalculationType = (int)TaxCalculationTypeEnum.FlatValue });

            taxCalculationHelperMock.Setup(helper => helper.CalculateFlatValueTax(It.IsAny<decimal>()))
                .Returns(expectedTaxAmount);

            // Act
            var result = await taxCalculationService.CalculateTaxAsync(calculateTaxDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Tax, Is.EqualTo(expectedTaxAmount));
        }

    }