using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Articles.Briefs;
using Iridium.Application.CQRS.Articles.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Articles.Queries;

public record GetArticlesByWorkspaceIdQuery : IRequest<ServiceResult<List<ArticleBriefDto>>>
{
    public long WorkspaceId { get; set; }
}

public class
    GetArticlesByWorkspaceIdQueryHandler : IRequestHandler<GetArticlesByWorkspaceIdQuery,
    ServiceResult<List<ArticleBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArticlesByWorkspaceIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<ArticleBriefDto>>> Handle(GetArticlesByWorkspaceIdQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Article.Where(x => x.Deleted != true && x.WorkspaceId == request.WorkspaceId)
            .OrderByDescending(x => x.Id)
            .ProjectTo<ArticleBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ServiceResult<List<ArticleBriefDto>>(dbResult);
    }
}