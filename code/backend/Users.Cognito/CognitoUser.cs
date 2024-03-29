﻿using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Staticsoft.ServerlessPass.Services;

namespace Staticsoft.ServerlessPass.Users.Cognito;

public class CognitoUser : User, ServiceStatus
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
            MessageAction = MessageActionType.SUPPRESS
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
        var response = await Client.InitiateAuthAsync(new InitiateAuthRequest()
        {
            AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
            ClientId = Options.ClientId,
            AuthParameters = new()
            {
                { "USERNAME", username },
                { "PASSWORD", password }
            }
        });
        return new AuthenticatedUser(
            accessToken: response.AuthenticationResult.IdToken,
            refreshToken: response.AuthenticationResult.RefreshToken
        );
    }

    public async Task<AuthenticatedUser> LogIn(string refreshToken)
    {
        var response = await Client.InitiateAuthAsync(new InitiateAuthRequest()
        {
            AuthFlow = AuthFlowType.REFRESH_TOKEN_AUTH,
            ClientId = Options.ClientId,
            AuthParameters = new()
            {
                { "REFRESH_TOKEN", refreshToken }
            }
        });
        return new AuthenticatedUser(
            accessToken: response.AuthenticationResult.IdToken,
            refreshToken: refreshToken
        );
    }

    public Task Check()
        => Client.ListUsersAsync(new()
        {
            Limit = 0,
            UserPoolId = Options.UserPoolId
        });
}
