using AutoMapper;
using Iridium.Application.CQRS.Todos.Briefs;
using Iridium.Domain.Entities;

namespace Iridium.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Todo, TodoBriefDto>().ReverseMap();
    }
}