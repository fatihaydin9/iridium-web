using Iridium.Application.Mappings;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Attributes.Base;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Types;
using Iridium.Domain.Entities;

namespace Iridium.Application.CQRS.Articles.Briefs;

[EndpointSettings("Articles/Template", "Articles/PaginatedList", "Articles/GetList", "Articles/Get", "Articles/Insert",
    "Articles/Update", "Articles/Delete", "Articles/Delete", "Articles/GetDropdown")]
public abstract class ArticleBriefDto : BaseDto, IMapFrom<Article>
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