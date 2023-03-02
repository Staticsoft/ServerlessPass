namespace Staticsoft.SharpPass.Contracts;

public record PasswordProfile : PasswordProfileRequest
{
    public string Id { get; init; } = string.Empty;
    public string Created { get; init; } = string.Empty;
    public string Modified { get; init; } = string.Empty;
}
