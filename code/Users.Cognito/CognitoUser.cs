using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;

namespace Staticsoft.SharpPass.Users.Cognito;

public class CognitoUser : User
{
    readonly AmazonCognitoIdentityProviderClient Client;
    readonly CognitoOptions Options;

    public CognitoUser(AmazonCognitoIdentityProviderClient client, CognitoOptions options)
        => (Client, Options)
        = (client, options);

    public async Task Create(string username, string password)
    {
        await Client.AdminCreateUserAsync(new AdminCreateUserRequest
        {
            UserPoolId = Options.UserPoolId,
            Username = username,
            TemporaryPassword = password,
        });
        await Client.AdminSetUserPasswordAsync(new AdminSetUserPasswordRequest()
        {
            UserPoolId = Options.UserPoolId,
            Username = username,
            Password = password,
            Permanent = true
        });
    }

    public async Task<AuthenticatedUser> LogIn(string username, string password)
    {
        var response = await Client.AdminInitiateAuthAsync(new AdminInitiateAuthRequest()
        {
            AuthFlow = AuthFlowType.ADMIN_USER_PASSWORD_AUTH,
            UserPoolId = Options.UserPoolId,
            ClientId = Options.ClientId,
            AuthParameters = new()
            {
                { "USERNAME", username },
                { "PASSWORD", password }
            }
        });
        return ToAuthenticatedUser(response);
    }

    public async Task<AuthenticatedUser> LogIn(string refreshToken)
    {
        var response = await Client.AdminInitiateAuthAsync(new AdminInitiateAuthRequest()
        {
            AuthFlow = AuthFlowType.REFRESH_TOKEN_AUTH,
            UserPoolId = Options.UserPoolId,
            ClientId = Options.ClientId,
            AuthParameters = new()
            {
                { "REFRESH_TOKEN", refreshToken }
            }
        });
        return ToAuthenticatedUser(response);
    }

    static AuthenticatedUser ToAuthenticatedUser(AdminInitiateAuthResponse response)
        => new(response.AuthenticationResult.AccessToken, response.AuthenticationResult.RefreshToken);
}
