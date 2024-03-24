 
using Iridium.Application.CQRS.Categories.Commands.DeleteCategory;
using Iridium.Application.CQRS.Categories.Commands.InsertCategory;
using Iridium.Application.CQRS.Categories.Commands.UpdateCategory;
using Iridium.Application.CQRS.Categories.Queries;
using Iridium.Application.Roles;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Models;
using Iridium.Infrastructure.Utilities;
using Iridium.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iridium.Web.Controllers {

    public class CategoryController : ApiBaseController
    {
        [AllowAnonymous]
        [HttpGet("Template")]
        public ActionResult<TemplateModel> Template()
        {
            var templateModel = new TemplateModel()
            {
                FormComponents = AttributeHelper.GetDtoFormComponents<CategoryBriefDto>(),
                EndpointSettings = AttributeHelper.GetDtoEndPointSettings<CategoryBriefDto>()
            };

            return templateModel;
        }

        [AllowAnonymous]
        [HttpGet("Dropdown")]
        [IridiumRole(CategoryRole.Read)]
        public async Task<ActionResult<List<KeyValueDto<long, string>>>> Dropdown()
        {
            var list = await List(new GetCategoriesQuery());

            if (list == null || list.Value == null)
                return new ActionResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

            var dropDownList = DtoMapper.MapToKeyValueDtoList(list.Value, d => d.Id, d => d.Name);

            return dropDownList;
        }

        [HttpGet("Get")]
        [IridiumRole(CategoryRole.Read)]
        public async Task<ActionResult<CategoryBriefDto>> Get([FromQuery] GetCategoryByIdQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("List")]
        [IridiumRole(CategoryRole.Read)]
        public async Task<ActionResult<List<CategoryBriefDto>>> List([FromQuery] GetCategoriesQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("PaginatedList")]
        [IridiumRole(CategoryRole.Read)]
        public async Task<ActionResult<PaginatedList<CategoryBriefDto>>> PaginatedList([FromQuery] GetCategoriesWithPaginationQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost("Insert")]
        [IridiumRole(CategoryRole.Insert)]
        public async Task<ActionResult<long>> Insert([FromBody] InsertCategoryCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("Update")]
        [IridiumRole(CategoryRole.Update)]
        public async Task<IActionResult> Update([FromBody] UpdateCategoryCommand command)
        {
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPost("Delete")]
        [IridiumRole(CategoryRole.Delete)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryCommand(id));
            return Ok();
        }


    }


}