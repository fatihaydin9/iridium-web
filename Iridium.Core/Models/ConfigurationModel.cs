namespace Iridium.Core.Models;

public class BaseSettings
{
    public string Url { get; set; }
}

public class ConnectionStrings
{
    public string ApplicationDbContext { get; set; }
}

public class MailClientSettings
{
    public string Address { get; set; }
    public string Port { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Mail { get; set; }
}

public class JwtConfig
{
    public string SecretKey { get; set; }
}

public class AppSettings
{
    public BaseSettings Base { get; set; }
    public ConnectionStrings ConnectionStrings { get; set; }
    public MailClientSettings MailClientSettings { get; set; }
    public JwtConfig JwtConfig { get; set; }
}