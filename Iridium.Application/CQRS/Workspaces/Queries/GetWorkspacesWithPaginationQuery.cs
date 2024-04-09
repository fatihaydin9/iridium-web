using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.Mappings;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Extensions;
using MediatR;

namespace Iridium.Application.CQRS.Categories.Queries;

public record GetWorkspacesWithPaginationQuery : IRequest<PaginatedList<WorkspaceBriefDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetWorkspacesWithPaginationQueryHandler : IRequestHandler<GetWorkspacesWithPaginationQuery, PaginatedList<WorkspaceBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspacesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<WorkspaceBriefDto>> Handle(GetWorkspacesWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Workspace.Where(w => w.Deleted != true)
                                        .OrderByDescending(x => x.Id)
                                        .ProjectTo<WorkspaceBriefDto>(_mapper.ConfigurationProvider)
                                        .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
