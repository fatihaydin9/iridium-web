using FluentValidation.Results;
using Iridium.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Iridium.Web.Controllers.Base;

[ApiController]
[ApiExceptionFilter]
[Route("[controller]")]
public abstract class ApiBaseController : ControllerBase
{
    [NonAction]
    public ServiceResult<bool> GetSucceededResult()
    {
        var serviceResult = new ServiceResult<bool>();
        serviceResult.Data = true;
        serviceResult.Succeeded = true;
        serviceResult.Message = "Operation completed successfully.";
        return serviceResult;
    }

    [NonAction]
    public ServiceResult<bool> GetValidationFailedResult(List<ValidationFailure> validationFailures)
    {
        var serviceResult = new ServiceResult<bool>();
        serviceResult.Data = false;
        serviceResult.Succeeded = false;
        serviceResult.ValidationErrors = validationFailures;
        serviceResult.Message = "Operation unsuccessful.";
        return serviceResult;
    }

    #region Mediator

    private ISender? _mediator;
    protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetRequiredService<ISender>();

    #endregion
}