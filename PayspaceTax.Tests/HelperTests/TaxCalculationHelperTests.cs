using PayspaceTax.Domain.Helpers;

namespace PayspaceTax.Tests.HelperTests
{
    public class TaxCalculationHelperTests
    {
        private TaxCalculationHelper taxCalculationHelper;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            taxCalculationHelper = new TaxCalculationHelper();
        }
        

        /// <summary>
        /// Simple unit test to ensure that the progressive tax is being calculated correctly according to the rules defined
        /// </summary>
        /// <param name="annualIncome">The annual income</param>
        /// <param name="rate">The rate percentage</param>
        /// <param name="expectedTax">The expected tax amount</param>
        [Test]
        [TestCase(5000, 10, 500)]
        [TestCase(31983, 15, 4797.45)]
        [TestCase(41934, 25, 10483.50)]
        [TestCase(102587, 28, 28724.36)]
        [TestCase(185000, 33, 61050)]
        [TestCase(585000, 35, 204750)]
        public void CalculateProgressiveTax_TestCases(decimal annualIncome, decimal rate, decimal expectedTax)
        {
            var tax = taxCalculationHelper.CalculateProgressiveTax(annualIncome, rate);

            // Perform assertions
            Assert.That(tax, Is.EqualTo(expectedTax));
           
        }

        /// <summary>
        /// Simple unit test to ensure that the flat value tax is being calculated correctly according to the rules defined
        /// </summary>
        /// <param name="annualIncome">The annual income</param>
        /// <param name="expectedTax">The expected tax amount</param>
        [Test]
        [TestCase(105000, 5250)]
        [TestCase(109999.99, 5500)]
        [TestCase(201342.10, 10000)]
        [TestCase(505154.32, 10000)]
        [TestCase(205431.54, 10000)]
        public void CalculateFlatValueTax_TestCases(decimal annualIncome, decimal expectedTax)
        {
            var tax = taxCalculationHelper.CalculateFlatValueTax(annualIncome);
            
            Assert.That(tax, Is.EqualTo(expectedTax));
        }

        /// <summary>
        /// Simple unit test to ensure that the flat rate tax is being calculated correctly according to the rules defined
        /// </summary>
        /// <param name="annualIncome">The annual income</param>
        /// <param name="expectedTax">The expected tax amount</param>
        [Test]
        [TestCase(105325,18431.88)]
        [TestCase(90543.33,15845.08)]
        public void CalculateFlatRateTax_TestCases(decimal annualIncome, decimal expectedTax)
        {
            var tax = taxCalculationHelper.CalculateFlatRateTax(annualIncome);
            
            Assert.That(tax, Is.EqualTo(expectedTax));
        }
    }
}