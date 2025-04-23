using System.ComponentModel.DataAnnotations;

namespace web_app_MVC_01.Models;

public class Course
{
    public Course()
    {
        Enrollments = new HashSet<Enrollment>();
    }
    public int CourseID { get; set; }
    
    public string Title { get; set; } = string.Empty;

    public int Credits { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; } = new List<Enrollment>();
}
