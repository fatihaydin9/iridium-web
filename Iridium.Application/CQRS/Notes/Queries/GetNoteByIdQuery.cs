using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Notes.Queries;

public record GetNoteByIdQuery : IRequest<ServiceResult<NoteBriefDto>>
{
    public long Id { get; init; }
}

public class GetNoteByIdQueryHandler : IRequestHandler<GetNoteByIdQuery, ServiceResult<NoteBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNoteByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<NoteBriefDto>> Handle(GetNoteByIdQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Note.Where(x => x.Id == request.Id)
            .ProjectTo<NoteBriefDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return new ServiceResult<NoteBriefDto>(dbResult);
    }
}