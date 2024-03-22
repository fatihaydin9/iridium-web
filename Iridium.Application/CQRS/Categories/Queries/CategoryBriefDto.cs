using Iridium.Application.Mappings;
using Iridium.Domain.Common;
using Iridium.Domain.Entities;
using Iridium.Infrastructure.Attributes.Base;
using Iridium.Infrastructure.Constants;
using Iridium.Infrastructure.Types;

namespace Iridium.Application.CQRS.Categories.Queries;

[EndpointSettings("Category/Template", "Category/PaginatedList", "Category/GetList", "Category/Get", "Category/Insert",
    "Category/Update", "Category/Delete", "Category/Delete", "Category/GetDropdown")]
public class CategoryBriefDto : BaseDto, IMapFrom<Category>
{
    [FormComponent("Name", true, FormInputType.InputBox, true, true, true, 6, AttributeConstants.NO_MASK,
        AttributeConstants.NO_CASCADE)]
    public string Name { get; set; } = string.Empty;


    [FormComponent("Note", true, FormInputType.TextArea, true, true, true, 12, AttributeConstants.NO_MASK,
        AttributeConstants.NO_CASCADE)]
    public string Note { get; set; } = string.Empty;
}