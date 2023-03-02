namespace Staticsoft.SharpPass.Contracts;

public class Password
{
    public string Id { get; init; } = string.Empty;
    public string Login { get; init; } = string.Empty;
    public string Site { get; init; } = string.Empty;
    public bool Uppercase { get; init; }
    public bool Lowercase { get; init; }
    public bool Numbers { get; init; }
    public bool Symbols { get; init; }
    public int Length { get; init; }
    public int Counter { get; init; }
    public int Version { get; init; }
    public string Created { get; init; } = string.Empty;
    public string Modified { get; init; } = string.Empty;
}
