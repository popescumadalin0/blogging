namespace Models;

public class RefreshTokenRequest
{
    public string Username { get; set; }

    public string AccessToken { get; set; }

    public string RefreshToken { get; set; }
}