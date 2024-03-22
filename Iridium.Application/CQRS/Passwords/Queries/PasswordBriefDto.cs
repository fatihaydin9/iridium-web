using Iridium.Application.Mappings;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Attributes.Base;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Types;

namespace Iridium.Application.CQRS.Passwords.Queries;

[EndpointSettings("Password/Template", "Password/PaginatedList", "Password/GetList", "Password/Get", "Password/Insert",
    "Password/Update", "Password/Delete", "Password/Delete", "Password/GetDropdown")]
public abstract class PasswordBriefDto : BaseDto, IMapFrom<Password>
{
    public long CategoryId { get; set; }

    [FormComponent("Username", true, FormInputType.InputBox, true, true, true, 12, AttributeConstants.NO_MASK,
        AttributeConstants.NO_CASCADE)]
    public string UserName { get; set; } = string.Empty;

    public string Hash { get; set; } = string.Empty;

    public string UserKey { get; set; } = string.Empty;
}