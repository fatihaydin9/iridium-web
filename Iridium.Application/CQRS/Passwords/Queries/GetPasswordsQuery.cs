using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Passwords.Queries;

public record GetPasswordsQuery : IRequest<ServiceResult<List<PasswordBriefDto>>>
{
    
}

public class GetPasswordsQueryHandler : IRequestHandler<GetPasswordsQuery, ServiceResult<List<PasswordBriefDto>>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPasswordsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<List<PasswordBriefDto>>> Handle(GetPasswordsQuery request, CancellationToken cancellationToken)
    {
        var result = await _context.Password.Where(x => x.Deleted != true)
                                            .OrderByDescending(x => x.Id)
                                            .ProjectTo<PasswordBriefDto>(_mapper.ConfigurationProvider)
                                            .ToListAsync();

        return new ServiceResult<List<PasswordBriefDto>>(result);
    }
}
