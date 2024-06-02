using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Todos.Briefs;
using Iridium.Domain.Common;
using Iridium.Persistence.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Todos.Queries;

public record GetTodoByIdQuery : IRequest<ServiceResult<TodoBriefDto>>
{
    public long Id { get; set; }
}

public class GetTodoByIdQueryQueryHandler : IRequestHandler<GetTodoByIdQuery, ServiceResult<TodoBriefDto>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoByIdQueryQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<TodoBriefDto>> Handle(GetTodoByIdQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Todo
            .Where(w => w.Id == request.Id && w.Deleted != true)
            .ProjectTo<TodoBriefDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken);

        return new ServiceResult<TodoBriefDto>(dbResult);
    }
}