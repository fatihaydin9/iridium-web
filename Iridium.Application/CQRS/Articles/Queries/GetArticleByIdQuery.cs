using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Articles.Briefs;
using Iridium.Application.CQRS.Articles.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Articles.Queries;

public record GetArticleByIdQuery : IRequest<ServiceResult<ArticleBriefDto>>
{
    public long Id { get; init; }
}

public class GetArticleByIdQueryHandler : IRequestHandler<GetArticleByIdQuery, ServiceResult<ArticleBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArticleByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<ArticleBriefDto>> Handle(GetArticleByIdQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Article
            .Include(i => i.Concepts)
            .Include(i => i.ArticleKeywords)
            .ThenInclude(t => t.Article)    
            .Where(x => x.Id == request.Id)
            .ProjectTo<ArticleBriefDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return new ServiceResult<ArticleBriefDto>(dbResult);
    }
}