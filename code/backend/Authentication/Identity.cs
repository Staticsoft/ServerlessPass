namespace Staticsoft.ServerlessPass.Authentication;

public interface Identity
{
    string UserId { get; }
    string Email { get; }
}