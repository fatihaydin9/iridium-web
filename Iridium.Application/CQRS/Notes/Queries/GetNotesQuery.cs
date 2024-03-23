using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Notes.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Notes.Queries;

public record GetNotesQuery : IRequest<ServiceResult<List<NoteBriefDto>>>
{
}

public class GetNotesQueryHandler : IRequestHandler<GetNotesQuery, ServiceResult<List<NoteBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNotesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<NoteBriefDto>>> Handle(GetNotesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _context.Note.Where(x => x.Deleted != true)
            .OrderByDescending(x => x.Id)
            .ProjectTo<NoteBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ServiceResult<List<NoteBriefDto>>(result);
    }
}