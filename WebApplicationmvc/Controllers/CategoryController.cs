using Microsoft.AspNetCore.Mvc;
using WebApplicationmvc.Data;
using WebApplicationmvc.Models;

namespace WebApplicationmvc.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objcatlist  = _db.Categories; //select *
            return View(objcatlist);
        }
        public IActionResult Create() 
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            //server side custom validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("Custom", "The Name and Display order cannot be same.");
                ModelState.AddModelError("Name", "The Name and Display order cannot be same."); // this will display under name element
            }
            //server side validation
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); //insert
                _db.SaveChanges(); //save inorder to literally insert otherwise it won't
                TempData["success"] = "Category created.";
                return RedirectToAction("Index");
            }
            return View(obj);
            //return RedirectToAction("Index","other controller");
            //return View();
        }
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            var catform = _db.Categories.Find(id);

            if (catform == null)
            {
                return NotFound();
            }
            return View(catform);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            //server side custom validation
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                //ModelState.AddModelError("Custom", "The Name and Display order cannot be same.");
                ModelState.AddModelError("Name", "The Name and Display order cannot be same."); // this will display under name element
            }
            //server side validation
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); //update
                _db.SaveChanges(); //save inorder to literally update otherwise it won't
                TempData["success"] = "Category updated.";
                return RedirectToAction("Index");
            }
            return View(obj);
            //return RedirectToAction("Index","other controller");
            //return View();
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var catform = _db.Categories.Find(id);

            if (catform == null)
            {
                return NotFound();
            }
            return View(catform);
        }
        //POST
        [HttpPost,ActionName("Delete")] //altername to deletepost method only runs when post request is triggered
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            //server side custom validation
            var catform = _db.Categories.Find(id);
            if (catform == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(catform); //remove
            _db.SaveChanges(); //save inorder to literally remove otherwise it won't
            TempData["success"] = "Category deleted.";
            return RedirectToAction("Index");

            //return View(obj);
            //return RedirectToAction("Index","other controller");
            //return View();
        }
    }
}
