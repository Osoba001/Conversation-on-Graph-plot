using System.ComponentModel.DataAnnotations.Schema;

namespace GroupChatDemo.Database.Entities
{
    internal class Conversation
    {
        public Guid Id { get; set; }
        public required double XCoordinate { get; set; }
        public required double YCoordinate { get; set; }
        public required Guid GraphPlotId { get; set; }
        [ForeignKey(nameof(GraphPlotId))]
        public GraphPlot GraphPlot { get; set; }
    }
}
