namespace SignalRChatApp.Domain.Entities.Dtos;

public sealed class UserLoginDto
{
    public string? UserName { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; }
}
