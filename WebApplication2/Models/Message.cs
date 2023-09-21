namespace GroupChatDemo.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Sent { get; set; }
        public int CreatedTimeStamp { get; set; }
        public User Sender { get; set; }
        public Conversation Conversation { get; set; }
    }
}
