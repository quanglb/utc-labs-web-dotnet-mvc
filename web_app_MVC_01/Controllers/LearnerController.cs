using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using web_app_MVC_01.Models;

namespace web_app_MVC_01.Controllers;

[Route("Learner")]
public class LearnerController : Controller
{
    private SchoolContext db;

    public LearnerController(SchoolContext _db)
    {
        db = _db;
    }

    #region View

    // [Route("List")]
    // public IActionResult Index()
    // {
    //     var data = db.Learners.Include(x => x.Major).ToList();
    //     return View(data);
    // }
    
    // [Route("List")]
    // public IActionResult Index(int page = 1)
    // {
    //     var learners = db.Learners.AsQueryable(); // giữ IQueryable để tối ưu
    //
    //     // Tính số trang
    //     int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
    //     ViewBag.pageNum = pageNum;
    //     ViewBag.page = page; // gửi luôn trang hiện tại nếu muốn highlight
    //
    //     // Lấy dữ liệu trang hiện tại
    //     var result = learners
    //         .Skip(pageSize * (page - 1))
    //         .Take(pageSize)
    //         .Include(x => x.Major)
    //         .ToList();
    //
    //     return View(result);
    // }

    #endregion

    #region Create

    [HttpGet("Add")]
    public IActionResult Create()
    {
        var majors = new List<SelectListItem>(); // cách 1
        foreach (var item in db.Majors)
        {
            majors.Add(new SelectListItem { Text = item.MajorName, Value = item.MajorID.ToString() });
        }

        ViewBag.MajorID = majors;

        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); // cách 2
        return View();
    }

    [HttpPost("Add")]
    [ValidateAntiForgeryToken]
    public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
    {
        if (ModelState.IsValid)
        {
            db.Learners.Add(learner);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
        return View();
    }

    #endregion

    #region Update

    [HttpGet("Edit")]
    public IActionResult Edit(int id)
    {
        if (id == null || db.Learners == null)
        {
            return NotFound();
        }

        var learner = db.Learners.Find(id);
        if (learner == null)
        {
            return NotFound();
        }

        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
    {
        if (id != learner.LearnerID)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                db.Update(learner);
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LearnerExists(learner.LearnerID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToAction(nameof(Index));
        }

        ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
        return View(learner);
    }

    private bool LearnerExists(int id)
    {
        return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
    }

    #endregion

    #region Remove

    public IActionResult Delete(int id)
    {
        if (id == null || db.Learners == null)
        {
            return NotFound();
        }

        var learner = db.Learners.Include(l => l.Major)
            .Include(e => e.Enrollments)
            .FirstOrDefault(m => m.LearnerID == id);

        if (learner == null)
        {
            return NotFound();
        }

        if (learner.Enrollments.Count() > 0)
        {
            return Content("This learner has some enrollments, can't delete!");
        }

        return View(learner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (db.Learners == null)
        {
            return Problem("Entity set 'Learners' is null.");
        }

        var learner = db.Learners.Find(id);
        if (learner != null)
        {
            db.Learners.Remove(learner);
        }

        db.SaveChanges();
        return RedirectToAction("Index");
    }

    #endregion

    private int pageSize = 3;
    [Route("List")]
    public IActionResult Index(int? mid)
    {
        var learners = db.Learners.ToList();
        
        if (mid != null)
        {
             learners = db.Learners.Where(x => x.MajorID == mid).Include(x => x.Major).ToList();
        }

        int pageNum = (int)Math.Ceiling((learners.Count) / (float)pageSize);

        ViewBag.pageNum = pageNum;
        var resp = learners.Take(pageSize).ToList();
        
        return View(resp);
        
    }
    
    [HttpGet("LearnerFilter")]
    public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
    {
        //lấy toàn bộ learners trong dbset chuyển về IQuerrable<Learner> để dùng Lingq
        var learners = (IQueryable<Learner>)db.Learners;
        //lấy chỉ số trang, nếu chỉ số trang null thì gán ngầm định bằng 1
        int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
        //nếu có mid thì lọc learner theo mid (chuyên ngành)
        if (mid != null)
        {
            learners = learners.Where(l => l.MajorID == mid); //lọc
            ViewBag.mid = mid; //gửi mid về view để ghi lại trên nav-trang
        }
        //nếu có keyword thì tìm kiếm theo tên
        if (!string.IsNullOrEmpty(keyword))
        {
            learners = learners.Where(l => l.FirstMidName.ToLower().Contains(keyword.ToLower())); //tìm kiếm
            ViewBag.keyword = keyword; //gửi keyword về view để ghi lại trên nav-trang
        }
        //tính số trang
        int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
        ViewBag.pageNum = pageNum; //gửi số trang về view để hiển thị nav-trang
        //chọn dữ liệu trong trang hiện tại
        var result = learners.Skip(pageSize * (page - 1))
            .Take(pageSize)
            .Include(m => m.Major).ToList();
        return PartialView("LearnerTable", result);
    }

    [HttpGet("LearnerByMajorID")]
    public IActionResult LearnerByMajorID(int mid)
    {
        Console.WriteLine("mid: " + mid);
        var resp = db.Learners.Where(x => x.MajorID == mid).Include(x => x.Major).ToList();
        return PartialView("LearnerTable", resp);
    }
}