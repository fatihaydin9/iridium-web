using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Todos.Briefs;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Extensions;
using MediatR;

namespace Iridium.Application.CQRS.Todos.Queries;

public record GetTodosWithPaginationQuery : IRequest<ServiceResult<PaginatedList<TodoBriefDto>>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetArticlesWithPaginationQueryHandler : IRequestHandler<GetTodosWithPaginationQuery,
    ServiceResult<PaginatedList<TodoBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArticlesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PaginatedList<TodoBriefDto>>> Handle(GetTodosWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _context.Todo.Where(w => w.Deleted != true)
            .OrderByDescending(o => o.Id)
            .ProjectTo<TodoBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ServiceResult<PaginatedList<TodoBriefDto>>(result);
    }
}