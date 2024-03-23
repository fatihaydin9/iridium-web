using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Notes.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Extensions;
using MediatR;

namespace Iridium.Application.CQRS.Notes.Queries;

public record GetNotesWithPaginationQuery : IRequest<ServiceResult<PaginatedList<NoteBriefDto>>>
{
    public long CategoryId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetNotesWithPaginationQueryHandler : IRequestHandler<GetNotesWithPaginationQuery,
    ServiceResult<PaginatedList<NoteBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetNotesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PaginatedList<NoteBriefDto>>> Handle(GetNotesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Note.Where(x => x.CategoryId == request.CategoryId)
            .OrderByDescending(x => x.Id)
            .ProjectTo<NoteBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ServiceResult<PaginatedList<NoteBriefDto>>(dbResult);
    }
}