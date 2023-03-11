using Microsoft.AspNetCore.Mvc;
using WebApplicationmvc.Data;
using WebApplicationmvc.Models;

namespace WebApplicationmvc.Controllers
{
    public class CheckController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CheckController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Check> checks = _db.Checks;
            return View(checks);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Check check) 
        {
            if (ModelState.IsValid)
            {
                _db.Checks.Add(check); //insert
                _db.SaveChanges(); //save inorder to literally insert otherwise it won't
                TempData["success"] = "Gender created.";
                return RedirectToAction("Index");
            }
            return View(check);
        }
        public IActionResult Edit(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }
            var check = _db.Checks.Find(id);
            if(check == null)
            {
                return NotFound();
            }
            return View(check);
        }
    }
}
