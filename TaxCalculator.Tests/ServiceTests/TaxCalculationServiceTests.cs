using System.Linq.Expressions;
using Moq;
using TaxCalculator.Application.DTOs;
using TaxCalculator.Application.Interfaces.Repositories;
using TaxCalculator.Application.Services;
using TaxCalculator.Domain.Entities;
using TaxCalculator.Domain.Enums;
using TaxCalculator.Domain.Exceptions;
using TaxCalculator.Domain.Helpers;

namespace TaxCalculator.Tests.ServiceTests;

    [TestFixture]
    public class TaxCalculationServiceTests
    {
        private Mock<IRepository<ProgressiveTaxBracket>> progressiveTaxBracketRepositoryMock;
        private Mock<IRepository<PostalCodeTaxCalculationType>> postalCodeTaxCalculationTypeRepositoryMock;
        private Mock<TaxCalculationHelper> taxCalculationHelperMock; 
        private TaxCalculationService taxCalculationService;

        /// <summary>
        /// Ensuring we setup all mock instances for services & helpers being used
        /// </summary>
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

        /// <summary>
        /// Unit test that includes and valid postal and ensure that the service returns valid result dto with calculated tax amount
        /// </summary>
        [Test]
        public async Task CalculateTaxAsync_ValidPostalCode_ReturnsCalculateTaxDtoWithTax()
        {
            //const variables to easily reference
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

        /// <summary>
        /// Unit test with invalid postal code, ensuring that it throws an exception when no valid postal code is found
        /// </summary>
        /// <returns></returns>
        [Test]
        public Task CalculateTaxAsync_InvalidPostalCode_ThrowsException()
        {
            //const variables to easily reference
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
            var exception = Assert.ThrowsAsync<TaxCalculatorException>(() => taxCalculationService.CalculateTaxAsync(calculateTaxDto));
            Assert.That(exception?.Message, Is.EqualTo(expectedExceptionMessage));
            return Task.CompletedTask;  
        }

        /// <summary>
        /// Unit test with invalid progressive tax bracket, ensuring that it throws an exception when no valid tax bracket is found
        /// </summary>
        /// <returns></returns>
        [Test]
        public Task CalculateTaxAsync_ProgressiveTaxBracketNotFound_ThrowsException()
        {
            //const variables to easily reference
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
            var exception = Assert.ThrowsAsync<TaxCalculatorException>(() => taxCalculationService.CalculateTaxAsync(calculateTaxDto));
            Assert.That(exception?.Message, Is.EqualTo(expectedExceptionMessage));
            return Task.CompletedTask;
        }
        
        /// <summary>
        /// Unit test that ensures it returns correctly calculated tax of Flat Rate Type
        /// </summary>
        [Test]
        public async Task CalculateTaxAsync_FlatRateType_ReturnsCalculateTaxDtoWithTax()
        {
            //const variables to easily reference
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

        /// <summary>
        /// Unit test that ensures it returns correctly calculated tax of Flat Value Type
        /// </summary>
        [Test]
        public async Task CalculateTaxAsync_FlatValueType_ReturnsCalculateTaxDtoWithTax()
        {
            //const variables to easily reference
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