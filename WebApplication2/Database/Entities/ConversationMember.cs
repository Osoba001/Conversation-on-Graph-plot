using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupChatDemo.Database.Entities
{
    internal class ConversationMember
    {
        public required Guid ConversationId { get; set; }
        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; }
        public required Guid MemberId { get; set; }
        [ForeignKey(nameof(MemberId))]
        public User Members { get; set; }
    }

    internal class ConversationMemberEntityConfig : IEntityTypeConfiguration<ConversationMember>
    {
        public void Configure(EntityTypeBuilder<ConversationMember> builder)
        {
            builder.HasNoKey();
            builder.HasIndex(x => new { x.MemberId, x.ConversationId }).IsUnique();
        }
    }
}
