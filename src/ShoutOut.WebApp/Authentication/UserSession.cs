namespace ShoutOut.WebApp.Authentication;

public class UserSession
{
    public required string Email { get; init; }
    public required string Role { get; init; }
}