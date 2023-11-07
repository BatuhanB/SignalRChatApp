namespace SignalRChatApp.Domain.Entities.Dtos
{
    public sealed class UserRolesDto
    {
        public string Id { get; set; }
        public List<AssignRoleDto> Roles { get; set; }
    }
}
