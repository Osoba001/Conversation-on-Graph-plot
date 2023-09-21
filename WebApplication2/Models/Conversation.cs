namespace GroupChatDemo.Models
{
    public class Conversation
    {
        public int Id { get; set; }
        public double XCoordinate { get; set; }
        public double YCoordinate { get; set; }
        public List<Message> Messages { get; set; }
        public GraphPlot GraphPlot { get; set; }
    }
}
