namespace GroupChatDemo.Services.Commands
{
    public class RemoveUsersFromCoversationCommand
    {
        public Guid ConversationId { get; set; }
        public required List<Guid> UserIds { get; set; }
    }
}
