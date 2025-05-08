using Microsoft.AspNetCore.Mvc;
using web_app_MVC_01.Models;

namespace web_app_MVC_01.ViewComponents;

public class MajorViewComponent : ViewComponent
{
    private SchoolContext db;
    private List<Major> majors;

    public MajorViewComponent(SchoolContext db)
    {
        this.db = db;
        majors = this.db.Majors.ToList();
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("RenderMajor", majors);
    }
}