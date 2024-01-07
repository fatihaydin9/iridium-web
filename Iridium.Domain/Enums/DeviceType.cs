using System.ComponentModel.DataAnnotations;

namespace Iridium.Domain.Enums
{
    public enum DeviceType
    {
        [Display(Name = "Apple")]
        AppleDevice = 0,
        [Display(Name = "Android")]
        AndroidDevice = 1,
        [Display(Name = "PostmanClient")]
        PostmanClient = 2,
        [Display(Name = "WebDevice")]
        WebDevice = 3,
        [Display(Name = "Unknown")]
        Unknown = 9
    }
}
