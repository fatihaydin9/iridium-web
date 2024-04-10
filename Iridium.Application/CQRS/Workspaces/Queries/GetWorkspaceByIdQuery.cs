using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Workspaces.Briefs;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Workspaces.Queries;

public record GetWorkspaceByIdQuery : IRequest<ServiceResult<WorkspaceBriefDto>>
{
    public long Id { get; init; }
}

public class GetWorkspaceByIdQueryHandler : IRequestHandler<GetWorkspaceByIdQuery, ServiceResult<WorkspaceBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspaceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<WorkspaceBriefDto>> Handle(GetWorkspaceByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Article.Where(x => x.Id == request.Id)
            .ProjectTo<WorkspaceBriefDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? new WorkspaceBriefDto();

        return new ServiceResult<WorkspaceBriefDto>(result);
    }
}