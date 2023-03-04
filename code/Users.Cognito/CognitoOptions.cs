namespace Staticsoft.SharpPass.Users.Cognito;

public class CognitoOptions
{
    public readonly string UserPoolId;
    public readonly string ClientId;
    public readonly string ClientSecret;

    public CognitoOptions(string userPoolId, string clientId, string clientSecret)
        => (UserPoolId, ClientId, ClientSecret)
        = (userPoolId, clientId, clientSecret);
}
