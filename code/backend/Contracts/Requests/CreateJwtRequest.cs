namespace Staticsoft.ServerlessPass.Contracts;

public class CreateJwtRequest
{
    public string email { get; init; } = string.Empty;
    public string password { get; init; } = string.Empty;
}
