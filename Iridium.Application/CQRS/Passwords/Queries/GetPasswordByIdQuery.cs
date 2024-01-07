using AutoMapper;
using AutoMapper.QueryableExtensions;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Contexts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Iridium.Application.CQRS.Passwords.Queries;

public record GetPasswordByIdQuery : IRequest<ServiceResult<PasswordBriefDto>>
{
    public long Id { get; init; }
}

public class GetPasswordByIdQueryHandler : IRequestHandler<GetPasswordByIdQuery, ServiceResult<PasswordBriefDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetPasswordByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResult<PasswordBriefDto>> Handle(GetPasswordByIdQuery request, CancellationToken cancellationToken)
    {
        var dbResult =  await _context.Password.Where(x => x.Id == request.Id)
                                               .ProjectTo<PasswordBriefDto>(_mapper.ConfigurationProvider)
                                               .FirstOrDefaultAsync();

        return new ServiceResult<PasswordBriefDto>(dbResult);
    }
}
