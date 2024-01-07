namespace Iridium.Domain.Models.ResponseModels
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public DateTime ExpiresIn { get; set; }
    }
}
