using Iridium.Application.CQRS.Passwords.Commands.DeleteCategory;
using Iridium.Application.CQRS.Passwords.Commands.InsertPassword;
using Iridium.Application.CQRS.Passwords.Commands.UpdatePassword;
using Iridium.Application.CQRS.Passwords.Queries;
using Iridium.Application.Roles;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Models;
using Iridium.Infrastructure.Utilities;
using Iridium.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iridium.Web.Controllers;

public class PasswordController : ApiBaseController
{
    [HttpGet("Get")]
    [Authorize]
    [IridiumRole(PasswordRole.Read)]
    public async Task<ServiceResult<PasswordBriefDto>> Get([FromQuery] GetPasswordByIdQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("List")]
    [IridiumRole(PasswordRole.Read)]
    public async Task<ServiceResult<List<PasswordBriefDto>>> List([FromQuery] GetPasswordsQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("ListByCategoryId")]
    [IridiumRole(PasswordRole.Read)]
    public async Task<ServiceResult<List<PasswordBriefDto>>> ListByCategoryId([FromQuery] GetPasswordsByCategoryIdQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("PaginatedList")]
    [IridiumRole(PasswordRole.Read)]
    public async Task<ServiceResult<PaginatedList<PasswordBriefDto>>> PaginatedList([FromQuery] GetPasswordsWithPaginationQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpPost("Insert")]
    [IridiumRole(PasswordRole.Insert)]
    public async Task<ServiceResult<bool>> Insert([FromBody] InsertPasswordCommand command)
        => await Mediator.Send(command);


    [Authorize]
    [HttpPost("Update")]
    [IridiumRole(PasswordRole.Update)]
    public async Task<ServiceResult<bool>> Update([FromBody] UpdatePasswordCommand command)
        => await Mediator.Send(command);


    [Authorize]
    [HttpPost("Delete")]
    [IridiumRole(PasswordRole.Delete)]
    public async Task<ServiceResult<bool>> Delete(int id)
        => await Mediator.Send(new DeletePasswordCommand(id));


    #region Template & Dropdown

    [AllowAnonymous]
    [HttpGet("Template")]
    public ActionResult<TemplateModel> Template()
    {
        var templateModel = new TemplateModel()
        {
            FormComponents = AttributeHelper.GetDtoFormComponents<PasswordBriefDto>(),
            EndpointSettings = AttributeHelper.GetDtoEndPointSettings<PasswordBriefDto>()
        };

        return templateModel;
    }

    [AllowAnonymous]
    [HttpGet("Dropdown")]
    public async Task<ServiceResult<List<KeyValueDto<long, string>>>> Dropdown()
    {
        var serviceResult = await List(new GetPasswordsQuery());

        if (serviceResult == null || serviceResult.Data == null)
            return new ServiceResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

        var dropDownList = DtoMapper.MapToKeyValueDtoList(serviceResult.Data, d => d.Id, d => d.UserName);

        return new ServiceResult<List<KeyValueDto<long, string>>>(dropDownList);
    }

    [AllowAnonymous]
    [HttpGet("Cascade")]
    public async Task<ServiceResult<List<KeyValueDto<long, string>>>> Cascade([FromQuery] long categoryId)
    {
        var serviceResult = await ListByCategoryId(new GetPasswordsByCategoryIdQuery { CategoryId = categoryId });

        if (serviceResult == null || serviceResult.Data == null)
            return new ServiceResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

        var dropDownList = DtoMapper.MapToKeyValueDtoList(serviceResult.Data, d => d.Id, d => d.UserName);

        return new ServiceResult<List<KeyValueDto<long, string>>>(dropDownList);
    }

    #endregion

}
