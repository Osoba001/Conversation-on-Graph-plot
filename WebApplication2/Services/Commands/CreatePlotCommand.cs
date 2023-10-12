namespace GroupChatDemo.Services.Commands
{
    public class CreatePlotCommand
    {
        public required string Name { get; set; }
        public required Guid PlotInitiatorId { get; set; }
    }
}
