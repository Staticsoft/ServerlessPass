namespace Staticsoft.ServerlessPass.Contracts;

public class JwtResponse
{
    public string access { get; init; } = string.Empty;
    public string refresh { get; init; } = string.Empty;
}
