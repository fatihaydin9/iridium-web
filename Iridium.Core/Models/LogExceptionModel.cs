namespace Iridium.Core.Models;

[Serializable]
public class LogExceptionModel
{
    public string Tag { get; set; }

    public string Message { get; set; }

    public string StackTrace { get; set; }
}