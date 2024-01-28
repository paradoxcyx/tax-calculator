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
            const string postalCode = "7441";
            const decimal annualIncome = 5000;
            
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = postalCode,
                AnnualIncome = annualIncome
            };

            var postalCodeTaxCalculationType = new PostalCodeTaxCalculationType
            {
                PostalCode = postalCode,
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
            Assert.That(result.Tax, Is.EqualTo(expectedTaxAmount));
        }

        [Test]
        public Task CalculateTaxAsync_InvalidPostalCode_ThrowsException()
        {
            const string postalCode = "InvalidPostalCode";
            const decimal annualIncome = 50000;
            const string expectedExceptionMessage = $"{postalCode} is an invalid Postal Code";
            
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = postalCode, // Assuming this postal code is invalid
                AnnualIncome = annualIncome
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync((PostalCodeTaxCalculationType)null); // Simulating not finding a tax calculation type for the given postal code

            // Act and Assert
            var exception = Assert.ThrowsAsync<PaySpaceTaxException>(() => taxCalculationService.CalculateTaxAsync(calculateTaxDto));
            Assert.That(exception?.Message, Is.EqualTo(expectedExceptionMessage));
            return Task.CompletedTask;  
        }

        [Test]
        public Task CalculateTaxAsync_ProgressiveTaxBracketNotFound_ThrowsException()
        {
            const string postalCode = "ValidPostalCode";
            const decimal annualIncome = 50000;
            const string expectedExceptionMessage = "Progressive Tax Bracket does not exist";
            
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = postalCode,
                AnnualIncome = annualIncome
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync(new PostalCodeTaxCalculationType { PostalCode = postalCode, TaxCalculationType = (int)TaxCalculationTypeEnum.Progressive});

            progressiveTaxBracketRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<ProgressiveTaxBracket, bool>>>()))
                .ReturnsAsync((ProgressiveTaxBracket)null); // Simulating not finding a progressive tax bracket

            // Act and Assert
            var exception = Assert.ThrowsAsync<PaySpaceTaxException>(() => taxCalculationService.CalculateTaxAsync(calculateTaxDto));
            Assert.That(exception?.Message, Is.EqualTo(expectedExceptionMessage));
            return Task.CompletedTask;
        }
        
        [Test]
        public async Task CalculateTaxAsync_FlatRateType_ReturnsCalculateTaxDtoWithTax()
        {
            const decimal expectedTaxAmount = 8750;
            const string postalCode = "7000";
            const decimal annualIncome = 50000;
            
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = postalCode,
                AnnualIncome = annualIncome
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync(new PostalCodeTaxCalculationType { PostalCode = postalCode, TaxCalculationType = (int)TaxCalculationTypeEnum.FlatRate });

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
            const string postalCode = "A100";
            const decimal annualIncome = 50000;
            
            // Arrange
            var calculateTaxDto = new CalculateTaxDto
            {
                PostalCode = postalCode,
                AnnualIncome = annualIncome
            };

            postalCodeTaxCalculationTypeRepositoryMock.Setup(repo => repo.GetFirstByAsync(It.IsAny<Expression<Func<PostalCodeTaxCalculationType, bool>>>()))
                .ReturnsAsync(new PostalCodeTaxCalculationType { PostalCode = postalCode, TaxCalculationType = (int)TaxCalculationTypeEnum.FlatValue });

            taxCalculationHelperMock.Setup(helper => helper.CalculateFlatValueTax(It.IsAny<decimal>()))
                .Returns(expectedTaxAmount);

            // Act
            var result = await taxCalculationService.CalculateTaxAsync(calculateTaxDto);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Tax, Is.EqualTo(expectedTaxAmount));
        }

    }