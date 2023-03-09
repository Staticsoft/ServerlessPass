namespace Staticsoft.SharpPass.Contracts;

public record PasswordProfileRequest
{
    public string login { get; init; } = string.Empty;
    public string site { get; init; } = string.Empty;
    public bool uppercase { get; init; }
    public bool lowercase { get; init; }
    public bool numbers { get; init; }
    public bool symbols { get; init; }
    public int length { get; init; }
    public int counter { get; init; }
    public int version { get; init; }

    public bool digits
        => numbers;
}
