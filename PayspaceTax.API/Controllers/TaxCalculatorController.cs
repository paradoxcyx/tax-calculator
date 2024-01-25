using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayspaceTax.API.Shared.Models;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Services;

namespace PayspaceTax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController(ITaxCalculationService taxCalculationService, IMapper mapper) : ControllerBase
    {
        [HttpPost("CalculateTax")]
        public async Task<IActionResult> CalculateTax([FromBody] CalculateTaxInput input)
        {
            var calculationDto = mapper.Map<CalculateTaxDto>(input);
            var taxCalculation = await taxCalculationService.CalculateTaxAsync(calculationDto);

            return Ok();
        }

        [HttpGet("History")]
        public async Task<IActionResult> GetHistory()
        {
            await Task.CompletedTask;
            return Ok();
        }
    }
}
