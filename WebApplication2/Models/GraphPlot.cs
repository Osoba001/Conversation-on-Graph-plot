namespace GroupChatDemo.Models
{
    public class GraphPlot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Group Group { get; set; }
        public List<Conversation> Conversations { get; set; }
        public List<User> Members { get; set; }
    }
}
