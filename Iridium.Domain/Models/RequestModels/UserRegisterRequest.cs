using System.ComponentModel.DataAnnotations;

namespace Iridium.Domain.Models.RequestModels;

public class UserRegisterRequest
{
    [Required]
    public string MailAddress { get; set; }

    [Required]
    public string PhoneNumber { get; set; }

    [Required]
    [MinLength(8, ErrorMessage = "Password should be at least 8 characters long.")]
    public string Password { get; set; }

    [Required]
    public string PasswordConfirm { get; set; }
}