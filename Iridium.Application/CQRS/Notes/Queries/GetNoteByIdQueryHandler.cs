using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Notes.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Nots.Queries;

public record GetNotesByWorkspaceIdQuery : IRequest<ServiceResult<List<NoteBriefDto>>>
{
    public long WorkspaceId { get; set; }
}

public class
    GetNotesByWorkspaceIdQueryHandler : IRequestHandler<GetNotesByWorkspaceIdQuery,
    ServiceResult<List<NoteBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNotesByWorkspaceIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<NoteBriefDto>>> Handle(GetNotesByWorkspaceIdQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Note.Where(x => x.Deleted != true && x.WorkspaceId == request.WorkspaceId)
            .OrderByDescending(x => x.Id)
            .ProjectTo<NoteBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ServiceResult<List<NoteBriefDto>>(dbResult);
    }
}