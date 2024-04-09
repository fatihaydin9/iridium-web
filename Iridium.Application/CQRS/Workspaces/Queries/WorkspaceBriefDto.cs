using Iridium.Application.Mappings;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Attributes.Base;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Types;

namespace Iridium.Application.CQRS.Categories.Queries;

[EndpointSettings("Workspace/Template", "Workspace/PaginatedList", "Workspace/GetList", "Workspace/Get", "Workspace/Insert",
    "Workspace/Update", "Workspace/Delete", "Workspace/Delete", "Workspace/GetDropdown")]
public class WorkspaceBriefDto : BaseDto, IMapFrom<Workspace>
{
    [FormComponent("Name", true, FormInputType.InputText, true, true, true, 6, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]
    public string Name { get; set; } = string.Empty;


    [FormComponent("Note", true, FormInputType.TextArea, true, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]
    public string Note { get; set; } = string.Empty;
}