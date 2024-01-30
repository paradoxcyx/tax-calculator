using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TaxCalculator.Application.DTOs;
using TaxCalculator.Application.Interfaces.Repositories;
using TaxCalculator.Application.Interfaces.Services;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Application.Services;

public class TaxCalculationHistoryService(IRepository<TaxCalculationHistory> taxCalculationHistoryRepository, IMapper mapper) : ITaxCalculationHistoryService
{
    public async Task<List<TaxCalculationHistoryDto>> GetTaxCalculationHistoryAsync()
    {
        var history = await taxCalculationHistoryRepository
            .GetAll()
            .OrderByDescending(x => x.CalculatedDate)
            .ToListAsync();
        
        return mapper.Map<List<TaxCalculationHistoryDto>>(history);
    }

    public async Task AddAsync(TaxCalculationHistoryDto history)
    {
        var dbHistory = mapper.Map<TaxCalculationHistory>(history);
        await taxCalculationHistoryRepository.AddAsync(dbHistory);
    }
}