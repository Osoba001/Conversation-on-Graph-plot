namespace GroupChatDemo.Database.Entities
{
    internal class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public List<ConversationMember> ConversationMembers { get; set; } = new();
    }
}
