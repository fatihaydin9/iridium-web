using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Articles.Briefs;
using Iridium.Application.CQRS.Articles.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Articles.Queries;

public record GetArticlesQuery : IRequest<ServiceResult<List<ArticleBriefDto>>>
{
}

public class GetArticlesQueryHandler : IRequestHandler<GetArticlesQuery, ServiceResult<List<ArticleBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArticlesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<ArticleBriefDto>>> Handle(GetArticlesQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _context.Article.Where(x => x.Deleted != true)
            .OrderByDescending(x => x.Id)
            .ProjectTo<ArticleBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ServiceResult<List<ArticleBriefDto>>(result);
    }
}