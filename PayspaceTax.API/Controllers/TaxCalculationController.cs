using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PayspaceTax.API.Shared.Models;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Services;
using PayspaceTax.Domain.Exceptions;

namespace PayspaceTax.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculationController(ITaxCalculationService taxCalculationService, ITaxCalculationHistoryService taxCalculationHistoryService, IMapper mapper) : ControllerBase
    {
        [HttpPost("CalculateTax")]
        public async Task<IActionResult> CalculateTax([FromBody] CalculateTaxInput input)
        {
            if (input.AnnualIncome < 0)
                throw new PaySpaceTaxException("Annual Income cannot be less than zero");
            
            //Calculate tax by using business logic defined in the service
            var calculationDto = mapper.Map<CalculateTaxDto>(input);
            var taxCalculationResult = await taxCalculationService.CalculateTaxAsync(calculationDto);

            //Adding calculation history to database
            var history = mapper.Map<TaxCalculationHistoryDto>(taxCalculationResult);
            await taxCalculationHistoryService.AddAsync(history);
            
            return Ok(new GenericResponseModel<CalculateTaxDto> {Success = true, Data = calculationDto, Message = "Calculated tax successfully"});
        }

        [HttpGet("History")]
        public async Task<IActionResult> GetHistory()
        {
            var history = await taxCalculationHistoryService.GetTaxCalculationHistoryAsync();
            return Ok(new GenericResponseModel<List<TaxCalculationHistoryDto>> {Success = true, Data = history, Message = "Tax calculation history successfully retrieved"});
        }
    }
}
