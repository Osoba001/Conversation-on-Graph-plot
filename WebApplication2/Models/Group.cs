namespace GroupChatDemo.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<GraphPlot> GraphPlots { get; set; }
    }
}
