using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Notes.Queries;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Categories.Queries;

public record GetWorkspaceByIdQuery : IRequest<WorkspaceBriefDto>
{
    public long Id { get; init; }
}

public class GetWorkspaceByIdQueryHandler : IRequestHandler<GetWorkspaceByIdQuery, WorkspaceBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWorkspaceByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<WorkspaceBriefDto> Handle(GetWorkspaceByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Note.Where(x => x.Id == request.Id)
            .ProjectTo<WorkspaceBriefDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? new WorkspaceBriefDto();
    }
}