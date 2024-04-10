using Iridium.Application.CQRS.Articles.Briefs;
using Iridium.Application.CQRS.Articles.Commands;
using Iridium.Application.CQRS.Articles.Queries;
using Iridium.Application.Roles;
using Iridium.Domain.Common;
using Iridium.Infrastructure.Models;
using Iridium.Infrastructure.Utilities;
using Iridium.Web.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Iridium.Web.Controllers;

public class ArticleController : ApiBaseController
{
    [Authorize]
    [HttpGet("Get")]
    [IridiumRole(ArticleRole.Read)]
    public async Task<ServiceResult<ArticleBriefDto>> Get([FromQuery] GetArticleByIdQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("List")]
    [IridiumRole(ArticleRole.Read)]
    public async Task<ServiceResult<List<ArticleBriefDto>>> List([FromQuery] GetArticlesQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("ListByWorkspaceId")]
    [IridiumRole(ArticleRole.Read)]
    public async Task<ServiceResult<List<ArticleBriefDto>>> ListByWorkspaceId([FromQuery] GetArticlesByWorkspaceIdQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpGet("PaginatedList")]
    [IridiumRole(ArticleRole.Read)]
    public async Task<ServiceResult<PaginatedList<ArticleBriefDto>>> PaginatedList([FromQuery] GetArticlesWithPaginationQuery query)
        => await Mediator.Send(query);


    [Authorize]
    [HttpPost("Insert")]
    [IridiumRole(ArticleRole.Insert)]
    public async Task<ServiceResult<bool>> Insert([FromBody] InsertArticleCommand command)
        => await Mediator.Send(command);


    [Authorize]
    [HttpPost("Update")]
    [IridiumRole(ArticleRole.Update)]
    public async Task<ServiceResult<bool>> Update([FromBody] UpdateArticleCommand command)
        => await Mediator.Send(command);


    [Authorize]
    [HttpPost("Delete")]
    [IridiumRole(ArticleRole.Delete)]
    public async Task<ServiceResult<bool>> Delete(int id)
        => await Mediator.Send(new DeleteArticleCommand(id));


    #region Template & Dropdown

    [AllowAnonymous]
    [HttpGet("Template")]
    public ActionResult<TemplateModel> Template()
    {
        var templateModel = new TemplateModel()
        {
            FormComponents = AttributeHelper.GetDtoFormComponents<ArticleBriefDto>(),
            EndpointSettings = AttributeHelper.GetDtoEndPointSettings<ArticleBriefDto>()
        };

        return templateModel;
    }

    [AllowAnonymous]
    [HttpGet("Dropdown")]
    public async Task<ServiceResult<List<KeyValueDto<long, string>>>> Dropdown()
    {
        var serviceResult = await List(new GetArticlesQuery());

        if (serviceResult == null || serviceResult.Data == null)
            return new ServiceResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

        var dropDownList = DtoMapper.MapToKeyValueDtoList(serviceResult.Data, d => d.Id, d => d.Title);

        return new ServiceResult<List<KeyValueDto<long, string>>>(dropDownList);
    }

    [AllowAnonymous]
    [HttpGet("Cascade")]
    public async Task<ServiceResult<List<KeyValueDto<long, string>>>> Cascade([FromQuery] long workspaceId)
    {
        var serviceResult = await ListByWorkspaceId(new GetArticlesByWorkspaceIdQuery() { WorkspaceId = workspaceId });

        if (serviceResult == null || serviceResult.Data == null)
            return new ServiceResult<List<KeyValueDto<long, string>>>(new List<KeyValueDto<long, string>>());

        var dropDownList = DtoMapper.MapToKeyValueDtoList(serviceResult.Data, d => d.Id, d => d.Title);

        return new ServiceResult<List<KeyValueDto<long, string>>>(dropDownList);
    }

    #endregion

}
