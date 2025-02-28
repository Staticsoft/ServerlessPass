using Microsoft.AspNetCore.Http;

namespace Staticsoft.ServerlessPass.Authentication.ASP;

public class ClaimIdentity : Identity
{
    readonly IHttpContextAccessor Accessor;

    public ClaimIdentity(IHttpContextAccessor accessor)
        => Accessor = accessor;

    public string UserId
        => Context.Features.Get<Claims>().Get("sub");

    public string Email
        => Context.Features.Get<Claims>().Get("email");

    HttpContext Context
        => Accessor.HttpContext ?? throw UnexpectedNullException(nameof(Context));

    static NullReferenceException UnexpectedNullException(string propertyName)
        => new($"Cannot read identity when {propertyName} is null");
}
