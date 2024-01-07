using Iridium.Domain.Common;

namespace Iridium.Domain.Entities;

public class User : BaseEntity
{
    public User()
    {
        UserRoles = new HashSet<UserRole>();
    }

    public string MailAddress { get; set; }

    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public string ValidationKey { get; set; }

    public DateTime ValidationExpire { get; set; }

    public bool IsPremium { get; set; }

    public short UserState { get; set; }

    public DateTime? LastPaymentDate { get; set; }

    public virtual ICollection<UserRole> UserRoles { get; set; }

}