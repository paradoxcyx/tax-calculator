using Microsoft.AspNetCore.Mvc;
using PayspaceTax.Web.Shared.Models;

namespace PayspaceTax.Web.Controllers;

public class TaxCalculatorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult CalculateTax(string postalCode, decimal annualIncome)
    {
        // Create a model to hold the result
        TaxCalculationResultModel resultModel = new TaxCalculationResultModel
        {
            PostalCode = postalCode,
            AnnualIncome = annualIncome,
            TaxAmount = 0
        };

        // Pass the model to the view
        return View("Index", resultModel);
    }
}