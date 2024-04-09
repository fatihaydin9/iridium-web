using Iridium.Application.CQRS.Categories.Commands.DeleteWorkspace;
using Iridium.Application.CQRS.Notes.Commands.DeleteNote;
using Iridium.Application.CQRS.Notes.Commands.InsertNote;
using Iridium.Application.CQRS.Notes.Commands.UpdateNote;
using Iridium.Application.CQRS.Notes.Queries;
using Iridium.Application.CQRS.Nots.Queries;
using Iridium.Application.Roles;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Models;
using Iridium.Infrastructure.Utilities;
using Iridium.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iridium.Web.Controllers;

public class NoteController : ApiBaseController
{
    [Authorize]
    [HttpGet("Get")]
    [IridiumRole(NoteRole.Read)]
    public async Task<ServiceResult<NoteBriefDto>> Get([FromQuery] GetNoteByIdQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("List")]
    [IridiumRole(NoteRole.Read)]
    public async Task<ServiceResult<List<NoteBriefDto>>> List([FromQuery] GetNotesQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("ListByWorkspaceId")]
    [IridiumRole(NoteRole.Read)]
    public async Task<ServiceResult<List<NoteBriefDto>>> ListByWorkspaceId([FromQuery] GetNotesByWorkspaceIdQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("PaginatedList")]
    [IridiumRole(NoteRole.Read)]
    public async Task<ServiceResult<PaginatedList<NoteBriefDto>>> PaginatedList([FromQuery] GetNotesWithPaginationQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpPost("Insert")]
    [IridiumRole(NoteRole.Insert)]
    public async Task<ServiceResult<bool>> Insert([FromBody] InsertNoteCommand command)
        => await Mediator.Send(command);


    [Authorize]
    [HttpPost("Update")]
    [IridiumRole(NoteRole.Update)]
    public async Task<ServiceResult<bool>> Update([FromBody] UpdateNoteCommand command)
        => await Mediator.Send(command);


    [Authorize]
    [HttpPost("Delete")]
    [IridiumRole(NoteRole.Delete)]
    public async Task<ServiceResult<bool>> Delete(int id)
        => await Mediator.Send(new DeleteNoteCommand(id));


    #region Template & Dropdown

    [AllowAnonymous]
    [HttpGet("Template")]
    public ActionResult<TemplateModel> Template()
    {
        var templateModel = new TemplateModel()
        {
            FormComponents = AttributeHelper.GetDtoFormComponents<NoteBriefDto>(),
            EndpointSettings = AttributeHelper.GetDtoEndPointSettings<NoteBriefDto>()
        };

        return templateModel;
    }

    [AllowAnonymous]
    [HttpGet("Dropdown")]
    public async Task<ServiceResult<List<KeyValueDto<long, string>>>> Dropdown()
    {
        var serviceResult = await List(new GetNotesQuery());

        if (serviceResult == null || serviceResult.Data == null)
            return new ServiceResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

        var dropDownList = DtoMapper.MapToKeyValueDtoList(serviceResult.Data, d => d.Id, d => d.Title);

        return new ServiceResult<List<KeyValueDto<long, string>>>(dropDownList);
    }

    [AllowAnonymous]
    [HttpGet("Cascade")]
    public async Task<ServiceResult<List<KeyValueDto<long, string>>>> Cascade([FromQuery] long workspaceId)
    {
        var serviceResult = await ListByWorkspaceId(new GetNotesByWorkspaceIdQuery() { WorkspaceId = workspaceId });

        if (serviceResult == null || serviceResult.Data == null)
            return new ServiceResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

        var dropDownList = DtoMapper.MapToKeyValueDtoList(serviceResult.Data, d => d.Id, d => d.Title);

        return new ServiceResult<List<KeyValueDto<long, string>>>(dropDownList);
    }

    #endregion

}
