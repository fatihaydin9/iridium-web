namespace Iridium.Domain.Common;

public class ServiceResult<T>
{
    public ServiceResult()
    {
    }

    public ServiceResult(T data, string? message = null) 
    {
        Succeeded = true;
        if (message != null) Message = message;
        Data = data;
    }

    public ServiceResult(string message)  
    {
        Succeeded = false;
        Message = message;
    }

    public bool Succeeded { get; set; }
    
    public string? Message { get; set; } 
    
    public List<string>? Errors { get; set; } 
    
    public T Data { get; set; }
    
    
    
}

