using Microsoft.AspNetCore.Mvc;

namespace PayspaceTax.Web.Controllers;

public class TaxCalculatorController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View();
    }
}