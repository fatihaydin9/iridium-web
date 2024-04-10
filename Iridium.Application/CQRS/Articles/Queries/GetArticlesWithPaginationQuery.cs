using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Articles.Briefs;
using Iridium.Application.CQRS.Articles.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Extensions;
using MediatR;

namespace Iridium.Application.CQRS.Articles.Queries;

public record GetArticlesWithPaginationQuery : IRequest<ServiceResult<PaginatedList<ArticleBriefDto>>>
{
    public long WorkspaceId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetArticlesWithPaginationQueryHandler : IRequestHandler<GetArticlesWithPaginationQuery,
    ServiceResult<PaginatedList<ArticleBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArticlesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PaginatedList<ArticleBriefDto>>> Handle(GetArticlesWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Article.Where(x => x.WorkspaceId == request.WorkspaceId)
            .OrderByDescending(x => x.Id)
            .ProjectTo<ArticleBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ServiceResult<PaginatedList<ArticleBriefDto>>(dbResult);
    }
}