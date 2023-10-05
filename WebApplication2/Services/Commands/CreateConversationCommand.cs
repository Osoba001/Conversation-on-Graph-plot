namespace GroupChatDemo.Services.Commands
{
    public class CreateConversationCommand
    {
        public required double XCoordinate { get; set; }
        public required double YCoordinate { get; set; }
        public required Guid GraphPlotId { get; set; }
        public List<Guid> MembersId { get; set; } = new();

    }
}
