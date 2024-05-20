using Iridium.Application.Mappings;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes.Base;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Types;
using Iridium.Domain.Entities;

namespace Iridium.Application.CQRS.Articles.Briefs;

[EndpointSettings("Todo/Template", "Todo/PaginatedList", "Todo/GetList", "Todo/Get", "Todo/Insert",
    "Todo/Update", "Todo/Delete", "Todo/Delete", "Todo/GetDropdown")]
public abstract class TodoBriefDto : BaseDto, IMapFrom<Todo>
{
    [FormComponent("Content", true, FormInputType.InputText, true, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]
    public string Content { get; set; }

    [FormComponent("IsCompleted", true, FormInputType.BoolSwitch, false, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]
    public bool IsCompleted { get; set; }
}