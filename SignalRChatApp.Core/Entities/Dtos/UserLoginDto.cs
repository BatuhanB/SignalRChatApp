namespace SignalRChatApp.Domain.Entities.Dtos;

public sealed class UserLoginDto
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public bool RememberMe { get; set; }
}
