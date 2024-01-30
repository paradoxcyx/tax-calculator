using AutoMapper;
using TaxCalculator.Application.DTOs;
using TaxCalculator.Domain.Entities;

namespace TaxCalculator.Mapping.Profiles;

public class TaxCalculationHistoryProfile : Profile
{
    public TaxCalculationHistoryProfile()
    {
        CreateMap<CalculateTaxDto, TaxCalculationHistory>()
            .ForMember(dest => dest.Tax, opt => opt.MapFrom(src => src.Tax ?? 0));

        CreateMap<CalculateTaxDto, TaxCalculationHistoryDto>();
        
        CreateMap<TaxCalculationHistory, TaxCalculationHistoryDto>().ReverseMap();
    }
}