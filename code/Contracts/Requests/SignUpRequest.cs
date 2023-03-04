namespace Staticsoft.SharpPass.Contracts;

public class SignUpRequest
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
}