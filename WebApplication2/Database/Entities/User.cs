namespace GroupChatDemo.Database.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<Conversation> Conversions { get; set; } = new();
    }
}
