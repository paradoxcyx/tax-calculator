using AutoMapper;
using PayspaceTax.API.Shared.Models;
using PayspaceTax.Application.DTOs;

namespace PayspaceTax.Mapping.Profiles;

public class TaxCalculationProfile : Profile
{
    public TaxCalculationProfile()
    {
        CreateMap<CalculateTaxInput, CalculateTaxDto>();
    }
}