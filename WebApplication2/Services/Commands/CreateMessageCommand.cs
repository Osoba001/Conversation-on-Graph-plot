namespace GroupChatDemo.Services.Commands
{
    public class CreateMessageCommand
    {
        public required string Text { get; set; }
        public required Guid SendId { get; set; }
        public required Guid ConversationId { get; set; }
        public required string GroupName { get; set; }
        public required string SenderName { get; set; }
    }
}
