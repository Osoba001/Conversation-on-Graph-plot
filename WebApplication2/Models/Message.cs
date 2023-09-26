using System.ComponentModel.DataAnnotations.Schema;

namespace GroupChatDemo.Models
{
    public class Message
    {
        public int Id { get; set; }
        public required string Text { get; set; }
        public bool IsEdited { get; set; }
        public DateTime CreatedTime { get; set; }= DateTime.Now;
        public required int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User Sender { get; set; }
        public required int ConversationId { get; set; }
        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; }
    }
}
