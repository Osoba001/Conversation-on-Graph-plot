using GroupChatDemo.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace GroupChatDemo.Database
{
    internal class GroupChatDbContext : DbContext
    {
        public GroupChatDbContext(DbContextOptions<GroupChatDbContext> options) : base(options)
        {
            var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            if (dbCreator != null)
            {
                if (!dbCreator.CanConnect())
                    dbCreator.Create();
                if (!dbCreator.HasTables())
                    dbCreator.CreateTables();
            }

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new ConversationMemberEntityConfig().Configure(modelBuilder.Entity<ConversationMember>());
        }
        public DbSet<User> Users { get; set; }
        public virtual DbSet<Conversation> Conversation { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<GraphPlot> Plots { get; set; }
        public DbSet<ConversationMember> ConversationMembers { get; set; }
    }
}
