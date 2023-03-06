namespace Staticsoft.SharpPass.Contracts;

public record PasswordProfile : PasswordProfileRequest
{
    public string id { get; init; } = string.Empty;
    public string created { get; init; } = string.Empty;
    public string modified { get; init; } = string.Empty;
}
