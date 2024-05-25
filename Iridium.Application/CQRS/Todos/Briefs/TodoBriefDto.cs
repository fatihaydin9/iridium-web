using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes.Base;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Types;
using Iridium.Domain.Entities;

namespace Iridium.Application.CQRS.Todos.Briefs;

[EndpointSettings("Todos/Template", "Todos/PaginatedList", "Todos/GetList", "Todos/Get", "Todos/Insert",
    "Todos/Update", "Todos/Delete", "Todos/Delete", "Todos/GetDropdown")]
public class TodoBriefDto : BaseDto
{
    [FormComponent("Content", true, FormInputType.InputText, true, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]
    public string Content { get; set; }

    [FormComponent("IsCompleted", true, FormInputType.BoolSwitch, false, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]
    public bool IsCompleted { get; set; }
}