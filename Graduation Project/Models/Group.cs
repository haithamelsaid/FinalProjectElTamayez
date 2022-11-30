using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Models
{
    public class Group
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreationTime { get; set; }
        public virtual List<Post>? Posts{ get; set; }

        [ForeignKey("Admin")]
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
    }
}
