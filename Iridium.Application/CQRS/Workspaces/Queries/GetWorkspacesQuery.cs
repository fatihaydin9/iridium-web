using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Workspaces.Briefs;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Workspaces.Queries;

public record GetWorkspacesQuery : IRequest<ServiceResult<List<WorkspaceBriefDto>>>
{
}

public class GetWorkspacesQueryHandler : IRequestHandler<GetWorkspacesQuery, ServiceResult<List<WorkspaceBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspacesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<WorkspaceBriefDto>>> Handle(GetWorkspacesQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Workspace.Where(x => x.Deleted != true)
            .OrderByDescending(x => x.Id)
            .ProjectTo<WorkspaceBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);
        
        return new ServiceResult<List<WorkspaceBriefDto>>(result);
    }
}