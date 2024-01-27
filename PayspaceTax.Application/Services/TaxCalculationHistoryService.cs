using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Application.Interfaces.Services;
using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Application.Services;

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