using NUnit.Framework;
using Moq;
using PayspaceTax.Domain.Helpers;
using System.Diagnostics;

namespace PayspaceTax.Tests
{
    public class TaxCalculationHelperTests
    {
        private ITaxCalculationHelper _taxCalculationHelper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            // Your one-time setup logic goes here, if needed
        }

        [SetUp]
        public void Setup()
        {
            try
            {
                // Create a mock for ITaxCalculationHelper
                var taxCalculationHelperMock = new Mock<ITaxCalculationHelper>();

                // Set up any necessary behavior for the mock

                // Assign the mock to the private field
                _taxCalculationHelper = taxCalculationHelperMock.Object;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Setup failed: {ex.Message}");
                throw; // Rethrow the exception to indicate setup failure
            }
        }

        [Test]
        [TestCase(5000, 10, 50)]
        public void CalculateProgressiveTax_TestCases(decimal annualIncome, decimal rate, decimal expectedTax)
        {
            try
            {
                // Call the method under test using the mocked ITaxCalculationHelper
                var tax = _taxCalculationHelper.CalculateProgressiveTax(annualIncome, rate);

                // Perform assertions
                Assert.That(tax, Is.EqualTo(expectedTax));
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Test failed: {ex.Message}");
                throw; // Rethrow the exception to indicate test failure
            }
        }
    }
}