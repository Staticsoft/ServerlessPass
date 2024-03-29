﻿using Staticsoft.ServerlessPass.Users;

namespace Staticsoft.ServerlessPass.Server;

public class SignUpEndpoint : HttpEndpoint<SignUpRequest, SignUpResponse>
{
    readonly User User;

    public SignUpEndpoint(User user)
        => User = user;

    public async Task<SignUpResponse> Execute(SignUpRequest request)
    {
        await User.Create(request.email, request.password);
        return new();
    }
}
