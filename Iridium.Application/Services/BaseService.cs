using Iridium.Domain.Common;

namespace Iridium.Application.Services;

public class BaseService
{
    public ServiceResult<bool> GetSucceededResult()
    {
        var serviceResult = new ServiceResult<bool>();
        serviceResult.Data = true;
        serviceResult.Succeeded = true;
        serviceResult.Message = "Operation Completed successfully.";
        return serviceResult;
    }
    
    public ServiceResult<bool> GetFailedResult(string message)
    {
        var serviceResult = new ServiceResult<bool>();
        serviceResult.Data = false;
        serviceResult.Succeeded = false;
        serviceResult.Message = message;
        return serviceResult;
    }
}