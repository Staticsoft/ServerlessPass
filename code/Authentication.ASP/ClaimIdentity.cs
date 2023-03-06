using Microsoft.AspNetCore.Http;

namespace Staticsoft.SharpPass.Authentication.ASP;

public class ClaimIdentity : Identity
{
    readonly IHttpContextAccessor Accessor;

    public ClaimIdentity(IHttpContextAccessor accessor)
        => Accessor = accessor;

    public string UserId
        => Context.Features.Get<Claims>().Get("sub");

    HttpContext Context
        => Accessor.HttpContext ?? throw UnexpectedNullException(nameof(Context));

    static NullReferenceException UnexpectedNullException(string propertyName)
        => new($"Cannot read identity when {propertyName} is null");
}
