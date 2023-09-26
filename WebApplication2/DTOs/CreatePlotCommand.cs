namespace GroupChatDemo.DTOs
{
    public class CreatePlotCommand
    {
        public string Name { get; set; }
        public required int GroupId { get; set; }
    }
}
