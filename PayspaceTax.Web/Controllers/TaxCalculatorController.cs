using Microsoft.AspNetCore.Mvc;
using PayspaceTax.Web.Services;
using PayspaceTax.Web.Shared.Models;
using PayspaceTax.Web.Shared.Models.TaxCalculator;

namespace PayspaceTax.Web.Controllers;

public class TaxCalculatorController(IApiService api) : Controller
{
    public IActionResult Index()
    {
        return View(new TaxCalculationViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(TaxCalculationViewModel model)
    {
        if (!ModelState.IsValid)
            View(model);

        var result = await api.CalculateTax(model);

        return View(result);
    }
}