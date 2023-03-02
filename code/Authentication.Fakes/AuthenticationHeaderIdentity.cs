using Microsoft.AspNetCore.Http;

namespace Staticsoft.SharpPass.Authentication.Fakes;

public class AuthenticationHeaderIdentity : Identity
{
    readonly IHttpContextAccessor Accessor;

    public AuthenticationHeaderIdentity(IHttpContextAccessor accessor)
        => Accessor = accessor;

    public string UserId
        => Context.Request.Headers["Authentication"].Single().Replace("JWT ", string.Empty);

    HttpContext Context
        => Accessor.HttpContext ?? throw UnexpectedNullException(nameof(Context));

    static NullReferenceException UnexpectedNullException(string propertyName)
        => new($"Cannot read identity when {propertyName} is null");
}
