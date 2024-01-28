using Microsoft.AspNetCore.Mvc;
using PayspaceTax.Web.Shared.Models.TaxCalculation;
using PayspaceTax.Web.Shared.Services;

namespace PayspaceTax.Web.Controllers;

public class TaxCalculationController(TaxCalculationApiService api, ILogger<TaxCalculationController> logger) : Controller
{
    public IActionResult Index()
    {
        return View(new TaxCalculationViewModel());
    }

    public async Task<IActionResult> History()
    {
        var result = await api.GetHistory();
        
        return View(result!.Data);
    }

    [HttpPost]
    public async Task<IActionResult> Index(TaxCalculationViewModel model)
    {
        //First determine if form validation has passed
        if (!ModelState.IsValid)
            View(model);

        //Call endpoint to calculate tax and return back tax amount
        var result = await api.CalculateTax(model);
        
        //If the tax calculation failed, assign error message and return to view
        if (result is { Success: false })
        {
            model.ErrorMessage = result.Message;
            
            logger.LogInformation("Error on calculation endpoint call: {message}", result.Message);
            return View(model);
        }

        model.TaxData = result?.Data;
        model.ErrorMessage = null;
        
        return View(model);
    }
}