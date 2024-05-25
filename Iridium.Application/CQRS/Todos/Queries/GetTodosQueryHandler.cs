using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Todos.Briefs;
using Iridium.Application.CQRS.Todos.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Todos.Queries;

public record GetTodosQuery : IRequest<ServiceResult<List<TodoBriefDto>>>
{
}

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, ServiceResult<List<TodoBriefDto>>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(ApplicationDbContext context, IMapper mapper)
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