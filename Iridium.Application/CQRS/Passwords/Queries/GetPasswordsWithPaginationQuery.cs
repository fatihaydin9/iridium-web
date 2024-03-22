using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using Iridium.Infrastructure.Extensions;
using MediatR;

namespace Iridium.Application.CQRS.Passwords.Queries;

public record GetPasswordsWithPaginationQuery : IRequest<ServiceResult<PaginatedList<PasswordBriefDto>>>
{
    public long CategoryId { get; init; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetCategoriesWithPaginationQueryHandler : IRequestHandler<GetPasswordsWithPaginationQuery,
    ServiceResult<PaginatedList<PasswordBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCategoriesWithPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PaginatedList<PasswordBriefDto>>> Handle(GetPasswordsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Password.Where(x => x.CategoryId == request.CategoryId)
            .OrderByDescending(x => x.Id)
            .ProjectTo<PasswordBriefDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        return new ServiceResult<PaginatedList<PasswordBriefDto>>(dbResult);
    }
}