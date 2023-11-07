namespace SignalRChatApp.Domain.Entities.Dtos
{
    public sealed class AssignRoleDto
    {
        public string RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsAssigned { get; set; }
    }
}
