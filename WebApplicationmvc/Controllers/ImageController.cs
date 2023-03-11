using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApplicationmvc.Data;
using WebApplicationmvc.Models.ImageView;

namespace WebApplicationmvc.Controllers
{
    public class ImageController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment _env;

        public ImageController(ApplicationDbContext db,IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            IEnumerable<Image> images = _db.Images;
            return View(images);
        }
        public IActionResult Create() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Image image)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    string uniqueFileName = UploadImage(image);
                    var data = new Image()
                    {
                        Name = image.Name,
                        Path = uniqueFileName
                    };
                    _db.Images.Add(data);
                    _db.SaveChanges();
                    TempData["success"] = "Image Added!!";
                    return RedirectToAction("Index");
                }
                ModelState.AddModelError(string.Empty,"Model is not valid.");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            
            return View(image);
        }
        private string UploadImage(Image image)
        {
            string uniqueFileName = string.Empty;
            if(image.ImagePath != null)
            {
                string uploadFolder = Path.Combine(_env.WebRootPath,"Content/Images/");
                uniqueFileName = Guid.NewGuid().ToString()+"_"+image.ImagePath.FileName;
                string filePath = Path.Combine(uploadFolder,uniqueFileName);
                using(var fileStream = new FileStream(filePath,FileMode.Create))
                {
                    image.ImagePath.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            else
            {
                var data = _db.Images.SingleOrDefault(x => x.Id == id);
                if (data != null)
                {
                    string deletefolder = Path.Combine(_env.WebRootPath, "Content/Image/");
                    string currectimage = Path.Combine(Directory.GetCurrentDirectory(), deletefolder, data.Path);
                    if (currectimage != null)
                    {
                        if (System.IO.File.Exists(currectimage))
                        {
                            System.IO.File.Delete(currectimage);
                        }
                    }
                    _db.Images.Remove(data);
                    _db.SaveChanges();
                    TempData["success"] = "Image Deleted!";
                }
            }
            return RedirectToAction("Index");
        }
        public IActionResult Edit(int id)
        {
            var data = _db.Images.Where(e=>e.Id == id).SingleOrDefault();
            if(data != null)
            {
                return View(data);
            }
            else { return RedirectToAction("Index"); }
        }
        [HttpPost]
        public IActionResult Edit(Image image)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var data = _db.Images.Where(e => e.Id == image.Id).SingleOrDefault();
                    string uniqueFileName = string.Empty;
                    if (image.ImagePath != null)
                    {
                        if(data.Path != null)
                        {
                            string filePath = Path.Combine(_env.WebRootPath, "Content/Image/", data.Path);
                            if(System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }
                        }
                        uniqueFileName = UploadImage(image);
                    }
                    data.Name = image.Name;
                    if(image.ImagePath != null)
                    {
                        data.Path = uniqueFileName;
                    }
                    _db.Images.Update(data);
                    _db.SaveChanges();
                    TempData["success"] = "Record updated successfully!";
                }
                else
                {
                    return View(image);
                }
            }
            catch(Exception ex) 
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction("index");
        }
    }
}
