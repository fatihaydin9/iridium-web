using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Categories.Queries;

public record GetWorkspacesQuery : IRequest<List<WorkspaceBriefDto>>
{
}

public class GetWorkspacesQueryHandler : IRequestHandler<GetWorkspacesQuery, List<WorkspaceBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspacesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<WorkspaceBriefDto>> Handle(GetWorkspacesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Workspace.Where(x => x.Deleted != true)
            .OrderByDescending(x => x.Id)
            .ProjectTo<WorkspaceBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);
    }
}