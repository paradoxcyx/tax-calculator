using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayspaceTax.API.Shared.Models;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Services;

namespace PayspaceTax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculationController(ITaxCalculationService taxCalculationService, ITaxCalculationHistoryService taxCalculationHistoryService, IMapper mapper) : ControllerBase
    {
        [HttpPost("CalculateTax")]
        public async Task<IActionResult> CalculateTax([FromBody] CalculateTaxInput input)
        {
            var calculationDto = mapper.Map<CalculateTaxDto>(input);
            var taxCalculationResult = await taxCalculationService.CalculateTaxAsync(calculationDto);

            return Ok(taxCalculationResult);
        }

        [HttpGet("History")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await taxCalculationHistoryService.GetTaxCalculationHistoryAsync();
            return Ok(history);
        }
    }
}
