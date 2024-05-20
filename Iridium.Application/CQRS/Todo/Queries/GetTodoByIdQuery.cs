using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Articles.Briefs;
using Iridium.Application.CQRS.Articles.Queries;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Articles.Queries;

public record GetTodoByIdQuery : IRequest<ServiceResult<TodoBriefDto>>
{
    public long Id { get; set; }
}

public class GetTodoByIdQueryQueryHandler : IRequestHandler<GetTodoByIdQuery, ServiceResult<TodoBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodoByIdQueryQueryHandler(IApplicationDbContext context, IMapper mapper)
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
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return new ServiceResult<TodoBriefDto>(dbResult);
    }
}