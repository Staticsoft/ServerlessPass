using Microsoft.AspNetCore.Http;

namespace Staticsoft.ServerlessPass.Authentication.Fakes;

public class AuthorizationHeaderIdentity : Identity
{
    readonly IHttpContextAccessor Accessor;

    public AuthorizationHeaderIdentity(IHttpContextAccessor accessor)
        => Accessor = accessor;

    public string UserId
        => GetUserId();

    public string Email
        => throw new NotImplementedException();

    string GetUserId()
    {
        var value = Context.Request.Headers["Authorization"].Single();
        if (!value.StartsWith("Bearer ")) throw new ArgumentException();

        return value.Replace("Bearer ", string.Empty);
    }

    HttpContext Context
        => Accessor.HttpContext ?? throw UnexpectedNullException(nameof(Context));

    static NullReferenceException UnexpectedNullException(string propertyName)
        => new($"Cannot read identity when {propertyName} is null");
}
