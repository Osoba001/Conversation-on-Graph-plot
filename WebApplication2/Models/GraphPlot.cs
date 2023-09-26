using System.ComponentModel.DataAnnotations.Schema;

namespace GroupChatDemo.Models
{
    public class GraphPlot
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public required int GroupId { get; set; }
        [ForeignKey(nameof(GroupId))]
        public Group? Group { get; set; }
    }
}
