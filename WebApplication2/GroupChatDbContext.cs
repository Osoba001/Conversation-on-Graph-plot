using GroupChatDemo.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace GroupChatDemo
{
    public class GroupChatDbContext: DbContext
    {
        public GroupChatDbContext(DbContextOptions<GroupChatDbContext> options):base(options)
        {
            //var dbCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
            //if (dbCreator != null)
            //{
            //    if (!dbCreator.CanConnect())
            //        dbCreator.Create();
            //    if (!dbCreator.HasTables())
            //        dbCreator.CreateTables();
            //}
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GraphPlot> GraphPlots { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Message> Messages { get; set; }
    }
}
