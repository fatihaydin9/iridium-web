using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Application.CQRS.Notes.Queries;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Categories.Queries;

public record GetCategoryByIdQuery : IRequest<CategoryBriefDto>
{
    public long Id { get; init; }
}

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQuery, CategoryBriefDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CategoryBriefDto> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        return await _context.Note.Where(x => x.Id == request.Id)
            .ProjectTo<CategoryBriefDto>(_mapper.ConfigurationProvider)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken) ?? new CategoryBriefDto();
    }
}