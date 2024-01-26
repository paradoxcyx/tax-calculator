using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PayspaceTax.Web.Shared.Models;
using PayspaceTax.Web.Shared.Models.TaxCalculation;
using PayspaceTax.Web.Shared.Services;

namespace PayspaceTax.Web.Controllers;

public class TaxCalculationController(TaxCalculationApiService api) : Controller
{
    public IActionResult Index()
    {
        return View(new TaxCalculationViewModel());
    }

    public async Task<IActionResult> History()
    {
        var result = await api.GetHistory();
        
        if (result is not { Success: true })
        {
            //TODO: Throw error
        }
        
        return View(result!.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Index(TaxCalculationViewModel model)
    {
        if (!ModelState.IsValid)
            View(model);

        var result = await api.CalculateTax(model);

        if (result is not { Success: true })
        {
            RedirectToAction("Error", result?.Message);
            //TODO: Throw error
        }
        
        return View(result!.Data);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error(string? message)
    {
        return View(new ErrorViewModel { RequestId = message });
    }
}