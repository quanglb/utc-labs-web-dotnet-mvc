using System.ComponentModel.DataAnnotations;

namespace web_app_MVC_01.Models;

public class Enrollment
{
    public int EnrollmentID { get; set; }

    public int CourseID { get; set; }
    public Course? Course { get; set; }

    public int LearnerID { get; set; }
    public Learner? Learner { get; set; }

    public double? Grade { get; set; }
}
