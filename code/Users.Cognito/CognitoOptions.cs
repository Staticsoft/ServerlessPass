namespace Staticsoft.SharpPass.Users.Cognito;

public class CognitoOptions
{
    public readonly string UserPoolId;
    public readonly string ClientId;

    public CognitoOptions(string userPoolId, string clientId)
        => (UserPoolId, ClientId)
        = (userPoolId, clientId);
}
