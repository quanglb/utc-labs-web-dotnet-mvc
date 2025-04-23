using System.ComponentModel.DataAnnotations;

namespace web_app_MVC_01.Models;

public class Learner
{
    public Learner()
    {
        Enrollments = new HashSet<Enrollment>();
    }
    
    public int LearnerID { get; set; }

    [Required]
    [StringLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [StringLength(50)]
    public string FirstMidName { get; set; } = string.Empty;

    [DataType(DataType.Date)]
    public DateTime EnrollmentDate { get; set; }

    public int MajorID { get; set; }
    public Major? Major { get; set; }

    public ICollection<Enrollment> Enrollments { get; set; }
}
