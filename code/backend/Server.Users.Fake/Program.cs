using System.Text;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Use(Cors);

app.MapGet("/login", (string redirect_uri, string response_type, string scope) =>
{
    if (response_type != "code") return InvalidResponseType();
    if (scope != "openid") return InvalidScope();

    var page = $@"
<html>
  <body>
    <form method=""POST"" action=""/login"">
      <label for=""inputField"">Username:</label>
      <input type=""text"" id=""inputField"" name=""user"" />
      <input type=""hidden"" name=""redirect_uri"" value=""{redirect_uri}"" />
      <button type=""submit"">Login</button>
    </form>
  </body>
</html>
";
    return Results.Text(page, contentType: "text/html");
});

app.MapPost("/login", (HttpContext context) =>
{
    var request = ToLoginRequest(context.Request.Form);
    var code = $"{request.user}!{request.redirect_uri}".ToBase64();
    var redirectToUrl = $"{request.redirect_uri}?code={code}";
    return Results.Redirect(redirectToUrl);
});

app.MapPost("/oauth2/token", (HttpContext context) =>
{
    var request = ToAuthenticationRequest(context.Request.Form);
    if (request.grant_type != "authorization_code") return InvalidGrantType();
    if (request.client_id != "fake") return InvalidClientId();

    var code = request.code.FromBase64();
    var parts = code.Split('!');
    var (user, redirect_uri) = (parts[0], parts[1]);
    if (request.redirect_uri != redirect_uri) return InvalidRequestUri(request, redirect_uri);

    return Results.Json(new { id_token = user });
});

app.Run();

static LoginRequest ToLoginRequest(IFormCollection form) => new()
{
    redirect_uri = form["redirect_uri"],
    user = form["user"]
};

static AuthenticationRequest ToAuthenticationRequest(IFormCollection form) => new()
{
    client_id = form["client_id"],
    code = form["code"],
    grant_type = form["grant_type"],
    redirect_uri = form["redirect_uri"]
};

static IResult InvalidResponseType()
    => BadRequest("response_type should be 'code'");

static IResult InvalidScope()
    => BadRequest("scope should be 'openid'");

static IResult InvalidGrantType()
    => BadRequest("grant_type should be 'authorization_code' (hard-coded)");

static IResult InvalidClientId()
    => BadRequest("client_id should be 'fake' (read from configuration)");

static IResult InvalidRequestUri(AuthenticationRequest request, string redirect_uri)
    => BadRequest($"redirect_uri mismatch: received '{request.redirect_uri}' but expected '{redirect_uri}'");

static IResult BadRequest(string message)
    => Results.BadRequest(new { message = message });

static Task Cors(HttpContext context, Func<Task> next)
{
    if (!context.Request.Headers.Origin.Any()) return next();

    var origin = context.Request.Headers.Origin.Single();
    if (AllowedOrigins().Contains(origin))
    {
        context.Response.Headers["Access-Control-Allow-Origin"] = origin;
    }
    context.Response.Headers["Access-Control-Allow-Headers"] = "content-type, accept, origin, authorization";
    context.Response.Headers["Access-Control-Allow-Methods"] = "GET, POST, PUT, DELETE, OPTIONS";

    if (context.Request.Method != "OPTIONS") return next();

    context.Response.StatusCode = 200;
    return context.Response.CompleteAsync();
}

static string[] AllowedOrigins()
    => Configuration("CrossOriginDomains").Split(',');

static string Configuration(string name)
     => Environment.GetEnvironmentVariable(name) ?? throw new NullReferenceException($"Environment varialbe {name} is not set");

class LoginRequest
{
    public string redirect_uri { get; init; } = string.Empty;
    public string user { get; init; } = string.Empty;
}

class AuthenticationRequest
{
    public string grant_type { get; init; } = string.Empty;
    public string client_id { get; init; } = string.Empty;
    public string code { get; init; } = string.Empty;
    public string redirect_uri { get; init; } = string.Empty;
}

public static class StringExtensions
{
    public static string ToBase64(this string value)
        => Convert.ToBase64String(Encoding.UTF8.GetBytes(value));

    public static string FromBase64(this string value)
        => Encoding.UTF8.GetString(Convert.FromBase64String(value));
}