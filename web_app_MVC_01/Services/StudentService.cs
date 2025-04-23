using web_app_MVC_01.Models;

namespace web_app_MVC_01.Services;

public class StudentService
{
    private List<Student> _students = new List<Student>()
    {
        new Student()
        {
            Id = 101, Name = "Le Ba Quang", Branch = Branch.IT, DateOfBorth = new DateTime(2000, 08, 02),
            Gender = Gender.Male, IsRegular = true, Address = "A1-2025", Email = "quanglb@gmail.com"
        },
        new Student()
        {
            Id = 101, Name = "Le Ba Tung", Branch = Branch.IT, DateOfBorth = new DateTime(2000, 02, 26),
            Gender = Gender.Male, IsRegular = true, Address = "A1-2025", Email = "tunglb@gmail.com"
        },
        new Student()
        {
            Id = 101, Name = "Le Ba Do", Branch = Branch.IT, DateOfBorth = new DateTime(2006, 02, 11),
            Gender = Gender.Male, IsRegular = true, Address = "A1-2025", Email = "dolb@gmail.com"
        },
        new Student()
        {
            Id = 101, Name = "John Terry", Branch = Branch.CE, DateOfBorth = new DateTime(1982, 08, 02),
            Gender = Gender.Male, IsRegular = true, Address = "A1-2023", Email = "terry@gmail.com"
        },
        new Student()
        {
            Id = 101, Name = "Cole Palmer", Branch = Branch.EE, DateOfBorth = new DateTime(1998, 08, 02),
            Gender = Gender.Male, IsRegular = false, Address = "A1-2021", Email = "palmer@gmail.com"
        },
        new Student()
        {
            Id = 101, Name = "Yamal", Branch = Branch.BE, DateOfBorth = new DateTime(2003, 08, 02),
            Gender = Gender.Male, IsRegular = false, Address = "A1-1999", Email = "yamal@gmail.com"
        }
    };

    public List<Student> GetStudents() => _students;
}