namespace Staticsoft.SharpPass.Users;

public class AuthenticatedUser
{
    public string AccessToken { get; }
    public string RefreshToken { get; }

    public AuthenticatedUser(string accessToken, string refreshToken)
        => (AccessToken, RefreshToken)
        = (accessToken, refreshToken);
}