using Iridium.Application.CQRS.Workspaces.Briefs;
using Iridium.Application.CQRS.Workspaces.Commands;
using Iridium.Application.CQRS.Workspaces.Queries;
using Iridium.Application.Roles;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Models;
using Iridium.Infrastructure.Utilities;
using Iridium.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iridium.Web.Controllers;

public class WorkspaceController : ApiBaseController
{
    [Authorize]
    [HttpGet("Get")]
    [IridiumRole(WorkspaceRole.Read)]
    public async Task<ServiceResult<WorkspaceBriefDto>> Get([FromQuery] GetWorkspaceByIdQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("List")]
    [IridiumRole(WorkspaceRole.Read)]
    public async Task<ServiceResult<List<WorkspaceBriefDto>>> List([FromQuery] GetWorkspacesQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("PaginatedList")]
    [IridiumRole(WorkspaceRole.Read)]
    public async Task<ServiceResult<PaginatedList<WorkspaceBriefDto>>> PaginatedList([FromQuery] GetWorkspacesWithPaginationQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpPost("Insert")]
    [IridiumRole(WorkspaceRole.Insert)]
    public async Task<ServiceResult<bool>> Insert([FromBody] InsertWorkspaceCommand command)
        => await Mediator.Send(command);


    [Authorize]
    [HttpPost("Update")]
    [IridiumRole(WorkspaceRole.Update)]
    public async Task<ServiceResult<bool>> Update([FromBody] UpdateWorkspaceCommand command)
        => await Mediator.Send(command);


    [Authorize]
    [HttpPost("Delete")]
    [IridiumRole(WorkspaceRole.Delete)]
    public async Task<ServiceResult<bool>> Delete(int id)
        => await Mediator.Send(new DeleteWorkspaceCommand(id));


    #region Template & Dropdown

    [AllowAnonymous]
    [HttpGet("Template")]
    public ActionResult<TemplateModel> Template()
    {
        var templateModel = new TemplateModel()
        {
            FormComponents = AttributeHelper.GetDtoFormComponents<WorkspaceBriefDto>(),
            EndpointSettings = AttributeHelper.GetDtoEndPointSettings<WorkspaceBriefDto>()
        };

        return templateModel;
    }

    [AllowAnonymous]
    [HttpGet("Dropdown")]
    public async Task<ServiceResult<List<KeyValueDto<long, string>>>> Dropdown()
    {
        var serviceResult = await List(new GetWorkspacesQuery());

        if (serviceResult == null || serviceResult.Data == null)
            return new ServiceResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

        var dropDownList = DtoMapper.MapToKeyValueDtoList(serviceResult.Data, d => d.Id, d => d.Name);

        return new ServiceResult<List<KeyValueDto<long, string>>>(dropDownList);
    }

    #endregion

}
