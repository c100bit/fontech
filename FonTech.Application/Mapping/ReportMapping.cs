using AutoMapper;
using FonTech.Domain.Dto.Report;
using FonTech.Domain.Entity;

namespace FonTech.Application.Mapping;

public class ReportMapping : Profile
{
    public ReportMapping()
    {
        CreateMap<Report, ReportDto>().ForCtorParam(
            "DateCreated", opt =>
                opt.MapFrom(src => src.CreatedAt)
        ).ReverseMap();
    }
}