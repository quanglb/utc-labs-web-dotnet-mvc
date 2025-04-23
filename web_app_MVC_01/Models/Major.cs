using System.ComponentModel.DataAnnotations;

namespace web_app_MVC_01.Models;

public class Major
{
    public Major()
    {
        Learners = new HashSet<Learner>();
    }

    public int MajorID { get; set; }

    [Required] [StringLength(100)] public string MajorName { get; set; } = string.Empty;

    public ICollection<Learner> Learners { get; set; }
}