using Application.Competitions.Models;
using AutoMapper;
using Domain.ValueObjects;

namespace Application.Infrastructure.Mappings;

public class EntryMappingProfile : Profile
{
    public EntryMappingProfile()
    {
        CreateMap<Entry, EntryModel>();
    }
}