using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.net_final2.Context;
using Project.net_final2.Models;

namespace Project.net_final2.Controllers
{
    
    public class CategoryController : Controller
    {
        private readonly ProjectContext db;
        public CategoryController(ProjectContext _db)
        {
            db = _db;
        }
        
        public IActionResult Index()
        {
            var Catgs = db.Categories;
            return View(Catgs);
        }


        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(Category category)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "All Fields Is Reqired");
                return View();
            }
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.FirstOrDefault(catg => catg.Id == id);
            if (category == null)
            {
                return RedirectToAction("Index");
            }

            return View(category);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "All Fields Is Reqired");
                return View();
            }
            db.Categories.Update(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var category = db.Categories.FirstOrDefault(catg => catg.Id == id);
            return View(category);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var category = db.Categories.FirstOrDefault(c => c.Id == id);
            if (category == null)
            { return RedirectToAction("Index"); }

            return View(category);
        }
        [Authorize]
        [HttpPost]
        public IActionResult Delete(int id, string confirm)
        {
            if (confirm == "yes")
            {
                var category = db.Categories.FirstOrDefault(c => c.Id  == id);
                if (category == null) { return RedirectToAction("Index"); }
                db.Categories.Remove(category);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
