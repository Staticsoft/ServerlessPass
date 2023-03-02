using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Staticsoft.SharpPass.Authentication.ASP;

public class ClaimIdentity : Identity
{
    readonly IHttpContextAccessor Accessor;

    public ClaimIdentity(IHttpContextAccessor accessor)
        => Accessor = accessor;

    public string UserId
        => IdentifierClaim.Value;

    Claim IdentifierClaim
        => Context.User.FindFirst(ClaimTypes.NameIdentifier) ?? throw UnexpectedNullException(nameof(IdentifierClaim));

    HttpContext Context
        => Accessor.HttpContext ?? throw UnexpectedNullException(nameof(Context));

    static NullReferenceException UnexpectedNullException(string propertyName)
        => new($"Cannot read identity when {propertyName} is null");
}
