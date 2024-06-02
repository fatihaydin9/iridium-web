using Iridium.Application.CQRS.Todos.Briefs;
using Iridium.Application.CQRS.Todos.Commands;
using Iridium.Application.CQRS.Todos.Queries;
using Iridium.Application.CQRS.Todos.Validators;
using Iridium.Application.Roles;
using Iridium.Core.Constants;
using Iridium.Core.Models;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Utilities;
using Iridium.Iridium.Infrastructure.Attributes;
using Iridium.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iridium.Web.Controllers;

public class TodoController : ApiBaseController
{
    [Authorize]
    [HttpGet("Get")]
    [IridiumRole(RoleParamCode.TodoList)]
    public async Task<ServiceResult<TodoBriefDto>> Get([FromQuery] GetTodoByIdQuery query)
        => await Mediator.Send(query);

    [Authorize]
    [HttpGet("List")]
    [IridiumRole(RoleParamCode.TodoList)]
    public async Task<ServiceResult<List<TodoBriefDto>>> List([FromQuery] GetTodosQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("PaginatedList")]
    [IridiumRole(RoleParamCode.TodoList)]
    public async Task<ServiceResult<PaginatedList<TodoBriefDto>>> PaginatedList([FromQuery] GetTodosWithPaginationQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpPost("Add")]
    [IridiumRole(RoleParamCode.TodoAdd)]
    public async Task<ServiceResult<bool>> Add([FromBody] InsertTodoCommand command)
    {
        var validator = new InsertTodoCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        
        if (!validationResult.IsValid)
            return GetValidationFailedResult(validationResult.Errors);

        return await Mediator.Send(command);
    }


    [Authorize]
    [HttpPost("Update")]
    [IridiumRole(RoleParamCode.TodoUpdate)]
    public async Task<ServiceResult<bool>> Update([FromBody] UpdateTodoCommand command)
    {
        var validator = new UpdateTodoCommandValidator();
        var validationResult = await validator.ValidateAsync(command);
        
        if (!validationResult.IsValid)
            return GetValidationFailedResult(validationResult.Errors);

        return await Mediator.Send(command);
    }


    [Authorize]
    [HttpPost("Delete")]
    [IridiumRole(RoleParamCode.TodoDelete)]
    public async Task<ServiceResult<bool>> Delete(int id)
        => await Mediator.Send(new DeleteTodoCommand(id));


    #region Template & Dropdown

    [AllowAnonymous]
    [HttpGet("Template")]
    public ActionResult<TemplateModel> Template()
    {
        var templateModel = new TemplateModel()
        {
            FormComponents = AttributeHelper.GetDtoFormComponents<TodoBriefDto>(),
            EndpointSettings = AttributeHelper.GetDtoEndPointSettings<TodoBriefDto>()
        };

        return templateModel;
    }

    [AllowAnonymous]
    [HttpGet("Dropdown")]
    public async Task<ServiceResult<List<KeyValueDto<long, string>>>> Dropdown()
    {
        var serviceResult = await List(new GetTodosQuery());

        if (serviceResult == null || serviceResult.Data == null)
            return new ServiceResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

        var dropDownList = DtoMapper.MapToKeyValueDtoList(serviceResult.Data, d => d.Id, d => d.Content);

        return new ServiceResult<List<KeyValueDto<long, string>>>(dropDownList);
    }

    #endregion

}
