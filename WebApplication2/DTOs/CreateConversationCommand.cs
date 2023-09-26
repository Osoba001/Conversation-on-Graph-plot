namespace GroupChatDemo.DTOs
{
    public class CreateConversationCommand
    {
        public required double XCoordinate { get; set; }
        public required double YCoordinate { get; set; }
        public required int GraphPlotId { get; set; }
        public List<int> MembersId { get; set; } = new();
    }
}
