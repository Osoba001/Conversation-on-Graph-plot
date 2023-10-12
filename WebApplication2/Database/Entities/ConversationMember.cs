using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace GroupChatDemo.Database.Entities
{
    internal class ConversationMember
    {
        public required Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
        public required Guid MemberId { get; set; }
        public User Member { get; set; }
    }

    internal class ConversationMemberEntityConfig : IEntityTypeConfiguration<ConversationMember>
    {
        public void Configure(EntityTypeBuilder<ConversationMember> builder)
        {
            builder.HasNoKey();
            builder.HasIndex(x => new { x.MemberId, x.ConversationId }).IsUnique();
            builder.HasOne(x=>x.Member).WithMany().HasForeignKey(x => x.MemberId).OnDelete(DeleteBehavior.NoAction);
        }
    }
}
