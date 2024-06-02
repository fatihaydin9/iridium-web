using Iridium.Core.Attributes.Base;
using Iridium.Core.Constants;
using Iridium.Core.Types;
using Iridium.Domain.Common;

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