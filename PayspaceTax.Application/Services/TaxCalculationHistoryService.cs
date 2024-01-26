﻿using AutoMapper;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Application.Interfaces.Repositories;
using PayspaceTax.Application.Interfaces.Services;
using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Application.Services;

public class TaxCalculationHistoryService(ITaxCalculationHistoryRepository taxCalculationHistoryRepository, IMapper mapper) : ITaxCalculationHistoryService
{
    public async Task<List<TaxCalculationHistoryDto>> GetTaxCalculationHistoryAsync()
    {
        var history = (await taxCalculationHistoryRepository.GetAllAsync()).OrderByDescending(x => x.CalculatedDate).ToList();
        return mapper.Map<List<TaxCalculationHistoryDto>>(history);
    }

    public async Task AddAsync(TaxCalculationHistoryDto history)
    {
        var dbHistory = mapper.Map<TaxCalculationHistory>(history);
        await taxCalculationHistoryRepository.AddAsync(dbHistory);
    }
}