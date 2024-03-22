using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Categories.Queries;

public record GetCategoriesQuery : IRequest<List<CategoryBriefDto>>
{
}

public class GetCategoriesQueryHandler : IRequestHandler<GetCategoriesQuery, List<CategoryBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CategoryBriefDto>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Category.Where(x => x.Deleted != true)
            .OrderByDescending(x => x.Id)
            .ProjectTo<CategoryBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}