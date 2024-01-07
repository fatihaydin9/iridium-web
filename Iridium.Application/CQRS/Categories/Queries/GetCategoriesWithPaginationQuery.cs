using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.Mappings;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Extensions;
using MediatR;

namespace Iridium.Application.CQRS.Categories.Queries;

public record GetCategoriesWithPaginationQuery : IRequest<PaginatedList<CategoryBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetCategoriesWithPaginationQuery, PaginatedList<CategoryBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CategoryBriefDto>> Handle(GetCategoriesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Category.Where(w => w.Deleted != true)
                                        .OrderByDescending(x => x.Id)
                                        .ProjectTo<CategoryBriefDto>(_mapper.ConfigurationProvider)
                                        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
