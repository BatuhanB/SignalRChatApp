namespace SignalRChatApp.Domain.Entities.Dtos;
public sealed class UserRegistrationDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
}
