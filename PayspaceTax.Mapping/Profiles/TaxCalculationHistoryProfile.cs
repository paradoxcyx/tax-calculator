using AutoMapper;
using PayspaceTax.Application.DTOs;
using PayspaceTax.Domain.Entities;

namespace PayspaceTax.Mapping.Profiles;

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