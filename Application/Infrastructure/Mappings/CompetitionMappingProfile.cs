using Application.Competitions.Models;
using AutoMapper;
using Domain.Entities;

namespace Application.Infrastructure.Mappings;

public class CompetitionMappingProfile : Profile
{
    public CompetitionMappingProfile()
    {
        CreateMap<Competition, CompetitionModel>()
            .ForMember(x => x.Winner, opt => opt.MapFrom(src => src.Winner.Name));
    }
}