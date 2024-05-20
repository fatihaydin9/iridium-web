using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Articles.Briefs;
using Iridium.Application.CQRS.Articles.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Articles.Queries;

public record GetTodosQuery : IRequest<ServiceResult<List<TodoBriefDto>>>
{
}

public class GetArticlesQueryHandler : IRequestHandler<GetTodosQuery, ServiceResult<List<TodoBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetArticlesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<TodoBriefDto>>> Handle(GetTodosQuery request,
        CancellationToken cancellationToken)
    {
        var result = await _context.Todo.Where(w => w.Deleted != true)
            .OrderByDescending(o => o.Id)
            .ProjectTo<TodoBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ServiceResult<List<TodoBriefDto>>(result);
    }
}