using System.ComponentModel.DataAnnotations.Schema;

namespace GroupChatDemo.Database.Entities
{
    internal class Plot
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required Guid PlotInitiatorId { get; set; }
        [ForeignKey(nameof(PlotInitiatorId))]
        public User PlotInitiator { get; set; }
    }
}
