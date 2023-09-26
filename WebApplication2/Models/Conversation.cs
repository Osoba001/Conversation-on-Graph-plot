using System.ComponentModel.DataAnnotations.Schema;

namespace GroupChatDemo.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public required double XCoordinate { get; set; }
        public required double YCoordinate { get; set; }
        public required int GraphPlotId { get; set; }
        [ForeignKey(nameof(GraphPlotId))]
        public GraphPlot? GraphPlot { get; set; }
        public List<User> Members { get; set; } = new();
        public List<Message> Messages { get; set; }=new();

    }
}
