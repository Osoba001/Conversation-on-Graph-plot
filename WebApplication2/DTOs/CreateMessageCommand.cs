namespace GroupChatDemo.DTOs
{
    public class CreateMessageCommand
    {
        public required string Text { get; set; }
        public required int UserId { get; set; }
        public required int ConversationId { get; set; }
    }
}
