namespace GroupChatDemo.Services.Commands
{
    public class AddUserToCoversationCommand
    {
        public Guid ConversationId { get; set; }
        public required List<Guid> UserIds { get; set; }
    }
}
