using Microsoft.AspNetCore.Mvc;
using PayspaceTax.API.Shared.Models;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Services;

namespace PayspaceTax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController(ITaxCalculationService taxCalculationService) : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpPost("CalculateTax")]
        public async Task<IActionResult> CalculateTax([FromBody] CalculateTaxInput input)
        {
            var taxCalculation = await taxCalculationService.CalculateTaxAsync(new CalculateTaxDto
            {
                AnnualIncome = input.AnnualIncome,
                PostalCode = input.PostalCode
            });

            return Ok();
        }
    }
}
