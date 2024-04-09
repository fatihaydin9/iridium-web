using Iridium.Application.Mappings;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes.Base;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Types;
using Iridium.Domain.Entities;

namespace Iridium.Application.CQRS.Notes.Queries;

[EndpointSettings("Note/Template", "Note/PaginatedList", "Note/GetList", "Note/Get", "Note/Insert",
    "Note/Update", "Note/Delete", "Note/Delete", "Note/GetDropdown")]
public abstract class NoteBriefDto : BaseDto, IMapFrom<Note>
{
    public long WorkspaceId { get; set; }

    [FormComponent("Title", true, FormInputType.TextArea, true, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]
    public string Title { get; set; }

    [FormComponent("Content", true, FormInputType.HtmlContent, true, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]
    public string Content { get; set; }

    [FormComponent("Summary", true, FormInputType.TextArea, true, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]   
    public string Summary { get; set; }
    
    [FormComponent("Private", true, FormInputType.BoolSwitch, true, true, true, 12, AttributeConfigurations.NoMask,
        AttributeConfigurations.NoCascade)]   
    public bool IsPrivate { get; set; }
    
}