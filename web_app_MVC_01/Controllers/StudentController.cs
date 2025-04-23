using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using web_app_MVC_01.Models;

namespace web_app_MVC_01.Controllers;

[Route("Students")]
public class StudentController : Controller
{
    private static List<Student> listStudent = new List<Student>();

    public StudentController()
    {
        if (!listStudent.Any())
        {
            listStudent = new List<Student>()
            {
                new Student()
                {
                    Id = 101, Name = "Le Ba Quang", Branch = Branch.IT, DateOfBorth = new DateTime(2000, 08, 02),
                    Gender = Gender.Male, IsRegular = true, Address = "A1-2025", Email = "quanglb@gmail.com"
                },
                new Student()
                {
                    Id = 102, Name = "Le Ba Tung", Branch = Branch.IT, DateOfBorth = new DateTime(2007, 02, 26),
                    Gender = Gender.Male, IsRegular = true, Address = "A1-2025", Email = "tunglb@gmail.com"
                },
                new Student()
                {
                    Id = 103, Name = "Le Ba Do", Branch = Branch.IT, DateOfBorth = new DateTime(2006, 02, 11),
                    Gender = Gender.Male, IsRegular = true, Address = "A1-2025", Email = "dolb@gmail.com"
                },
                new Student()
                {
                    Id = 104, Name = "John Terry", Branch = Branch.CE, DateOfBorth = new DateTime(1982, 08, 02),
                    Gender = Gender.Male, IsRegular = true, Address = "A1-2023", Email = "terry@gmail.com"
                },
                new Student()
                {
                    Id = 105, Name = "Cole Palmer", Branch = Branch.EE, DateOfBorth = new DateTime(1998, 08, 02),
                    Gender = Gender.Male, IsRegular = false, Address = "A1-2021", Email = "palmer@gmail.com"
                },
                new Student()
                {
                    Id = 106, Name = "Yamal", Branch = Branch.BE, DateOfBorth = new DateTime(2003, 08, 02),
                    Gender = Gender.Male, IsRegular = false, Address = "A1-1999", Email = "yamal@gmail.com"
                }
            };
        }
    }

    [Route("List")]
    public IActionResult Index()
    {
        return View(listStudent);
    }

    [HttpGet("Add")]
    public IActionResult Create()
    {
        ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        ViewBag.AllBranches = new List<SelectListItem>()
        {
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
        };
        return View();
    }

    [HttpPost("Add")]
    public IActionResult Create(Student s)
    {
        if (ModelState.IsValid)
        {
            s.Id = listStudent.Last().Id + 1;

            if (s.Avatar != null && s.Avatar.Length > 0)
            {
                var fileName = Path.GetFileName(s.Avatar.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    s.Avatar.CopyTo(stream);
                }

                s.AvatarUrl = fileName;
            }

            listStudent.Add(s);

            return RedirectToAction("Index");
        }

        ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
        ViewBag.AllBranches = new List<SelectListItem>()
        {
            new SelectListItem { Text = "None", Value = "0" },
            new SelectListItem { Text = "IT", Value = "1" },
            new SelectListItem { Text = "BE", Value = "2" },
            new SelectListItem { Text = "CE", Value = "3" },
            new SelectListItem { Text = "EE", Value = "4" }
        };
        return View();
    }

    [Route("ShowDemo")]
    public IActionResult ShowDemo(string name)
    {
        ViewBag.name = name;
        return View();
    }
}