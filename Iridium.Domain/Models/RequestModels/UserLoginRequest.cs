using System.ComponentModel.DataAnnotations;

namespace Iridium.Domain.Models.RequestModels;

public class UserLoginRequest
{
    [Required] public string MailAddress { get; set; }

    [Required] public string Password { get; set; }
}