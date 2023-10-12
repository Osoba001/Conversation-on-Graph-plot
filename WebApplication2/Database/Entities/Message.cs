using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupChatDemo.Database.Entities
{
    internal class Message
    {
        public Guid Id { get; set; }
        public required string Text { get; set; }
        public bool IsEdited { get; set; }
        public DateTime CreatedTime { get; set; } = DateTime.Now;
        public required Guid UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User Sender { get; set; }
        public required Guid ConversationId { get; set; }
        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; }
    }
    internal class MessageEntityConfiguratation : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.HasOne(x => x.Sender).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
