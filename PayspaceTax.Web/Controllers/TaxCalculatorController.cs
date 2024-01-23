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
    public IActionResult Index(string postalCode, decimal annualIncome)
    {
        // Create a model to hold the result
        var resultModel = new TaxCalculationResultModel
        {
            PostalCode = postalCode,
            AnnualIncome = annualIncome,
            TaxAmount = 0
        };

        return View(resultModel);
    }
}