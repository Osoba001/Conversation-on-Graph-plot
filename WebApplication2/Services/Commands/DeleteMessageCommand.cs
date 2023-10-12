namespace GroupChatDemo.Services.Commands
{
    public class DeleteMessageCommand
    {
        public required List<Guid> Ids { get; set; }
    }
}
