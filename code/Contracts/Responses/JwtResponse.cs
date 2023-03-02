namespace Staticsoft.SharpPass.Contracts;

public class JwtResponse
{
    public string Access { get; init; } = string.Empty;
    public string Refresh { get; init; } = string.Empty;
}
