using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Passwords.Queries;

public record GetPasswordsByCategoryIdQuery : IRequest<ServiceResult<List<PasswordBriefDto>>>
{
    public long CategoryId { get; set; }
}

public class
    GetPasswordsByCategoryIdQueryHandler : IRequestHandler<GetPasswordsByCategoryIdQuery,
    ServiceResult<List<PasswordBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPasswordsByCategoryIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<PasswordBriefDto>>> Handle(GetPasswordsByCategoryIdQuery request,
        CancellationToken cancellationToken)
    {
        var dbResult = await _context.Password.Where(x => x.Deleted != true && x.CategoryId == request.CategoryId)
            .OrderByDescending(x => x.Id)
            .ProjectTo<PasswordBriefDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken: cancellationToken);

        return new ServiceResult<List<PasswordBriefDto>>(dbResult);
    }
}