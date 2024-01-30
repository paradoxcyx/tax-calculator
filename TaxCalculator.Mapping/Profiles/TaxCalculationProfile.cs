using AutoMapper;
using TaxCalculator.API.Shared.Models;
using TaxCalculator.Application.DTOs;

namespace TaxCalculator.Mapping.Profiles;

public class TaxCalculationProfile : Profile
{
    public TaxCalculationProfile()
    {
        CreateMap<CalculateTaxInput, CalculateTaxDto>();
    }
}