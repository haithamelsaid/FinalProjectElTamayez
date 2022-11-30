using System.ComponentModel.DataAnnotations.Schema;

namespace Graduation_Project.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public SubjectYear Year { get; set; }
        public string? Image { get; set; }
        public virtual List<Teacher>? Teachers { get; set; }


        [ForeignKey("Admin")]
        public int? AdminId { get; set; }
        public Admin? Admin { get; set; }
    }

    public enum SubjectYear
    {
        First,
        Second,
        Third
    }
}
