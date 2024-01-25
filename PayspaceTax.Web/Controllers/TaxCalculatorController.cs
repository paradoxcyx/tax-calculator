using Microsoft.AspNetCore.Mvc;
using PayspaceTax.Web.Services;
using PayspaceTax.Web.Shared.Models;

namespace PayspaceTax.Web.Controllers;

public class TaxCalculatorController(ApiService api) : Controller
{
    public IActionResult Index()
    {
        return View(new TaxCalculationModel());
    }

    [HttpPost]
    public async Task<IActionResult> Index(TaxCalculationModel model)
    {
        if (!ModelState.IsValid)
            View(model);

        var result = await api.CalculateTax(model);

        return View(result);
    }
}