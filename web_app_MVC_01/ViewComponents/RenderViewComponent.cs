using Microsoft.AspNetCore.Mvc;
using web_app_MVC_01.Models;

namespace web_app_MVC_01.ViewComponents;

public class RenderViewComponent : ViewComponent
{
    private List<MenuItem> MenuItems = new();
    public RenderViewComponent()
    {
        MenuItems = new List<MenuItem>()
        {
            //new MenuItem() { Id = 1, Link = "/Branches/List", Name = "Branches" },
            new MenuItem() { Id = 1, Link = "/Students/List", Name = "Students" },
            //new MenuItem() { Id = 1, Link = "/Subjects/List", Name = "Subjects" },
            //new MenuItem() { Id = 1, Link = "/Courses/List", Name = "Courses" },
            new MenuItem() { Id = 1, Link = "/Learner/List", Name = "Learner" },
        };
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        return View("RenderLeftMenu", MenuItems);
    }
}