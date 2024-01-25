using Microsoft.AspNetCore.Mvc;
using PayspaceTax.Web.Services;
using PayspaceTax.Web.Shared.Models.TaxCalculation;

namespace PayspaceTax.Web.Controllers;

public class TaxCalculationController(TaxCalculationApiService api) : Controller
{
    public IActionResult Index()
    {
        return View(new TaxCalculationViewModel());
    }

    public async Task<IActionResult> History()
    {
        var history = await api.GetHistory();

        return View(history);
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