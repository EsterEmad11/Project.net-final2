
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.EntityFrameworkCore;
using Project.net_final2.Context;
using Project.net_final2.Models;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace Project.net_final2.Controllers
{
    
    public class BookController : Controller
    {
        private readonly ProjectContext db;

        private readonly IHostingEnvironment Host;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(ProjectContext _db , IHostingEnvironment _host , IWebHostEnvironment webHostEnvironment)
        {
            db = _db;
            Host = _host;
            _webHostEnvironment = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            var book = db.Books.Include(bk => bk.Category);
            return View(book);
        }


        //[HttpGet]
        //public IActionResult Create()
        //{
        //    ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
        //    return View();
        //}


        //[HttpPost]
        //public IActionResult Create(Book book, IFormFile ImageFile)
        //{
        //    ModelState.Remove("Category");
        //    if (!ModelState.IsValid)
        //    {
        //        ModelState.AddModelError("", "All fields are required.");
        //        ViewBag.Categories = new SelectList(db.Categories, "CategoryId", "Name");
        //        return View();
        //    }
        //    string fileName = string.Empty;
        //    // Handle Image Upload
        //    if (book.clientFile != null)
        //    {
        //        string myUpload = Path.Combine(Host.WebRootPath, "images");
        //        fileName = book.clientFile.FileName;
        //        string fullPath = Path.Combine(myUpload, fileName);
        //        book.clientFile.CopyTo(new FileStream(fullPath, FileMode.Create));
        //        book.ImagePath = fileName;
        //    }


        //    db.Books.Add(book);
        //    db.SaveChanges();

        //    return RedirectToAction("Index");
        //}




        //--------------------------------------------------------------------------------------
        [Authorize]
        [HttpGet]

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult Create(Book book)
        {
            ModelState.Remove("Category");

            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("", "All Fields Is Reqired");

                ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");

                return View();
            }
            string fileName = string.Empty;

            // Handle Image Upload
            if (book.clientFile != null)
            {
                // Set the path to the "wwwroot/images" folder
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");

                // Ensure the directory exists
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Generate a unique file name
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(book.clientFile.FileName);

                // Get the full file path
                string fullPath = Path.Combine(uploadFolder, fileName);

                // Save the file to the server
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    book.clientFile.CopyTo(fileStream);
                }

                // Save the relative image path to the database
                book.ImagePath = "/images/" + fileName;
            }

            // Save the book details to the database
            db.Add(book);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        //---------------------------------------------------------------------------------------------
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var book = db.Books.Include(bk => bk.Category).FirstOrDefault(bk => bk.BookId == id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");
            return View(book);
        }


        [HttpPost]
        public IActionResult Edit(int id, Book book)
        {
            ModelState.Remove("Category");
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("", "All Fields Is Reqired");

                ViewBag.Categories = new SelectList(db.Categories, "Id", "Name");

                return View();
            }

            string fileName = book.ImagePath; // Keep the original image if a new one isn't uploaded

            // Handle Image Upload
            if (book.clientFile != null)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }

                // Generate a unique file name
                fileName = Guid.NewGuid().ToString() + Path.GetExtension(book.clientFile.FileName);
                string fullPath = Path.Combine(uploadFolder, fileName);

                // Save the new file
                using (var fileStream = new FileStream(fullPath, FileMode.Create))
                {
                    book.clientFile.CopyTo(fileStream);
                }

                // Update the ImagePath to the new file
                book.ImagePath = "/images/" + fileName;
            }

            
            db.Update(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewDetails(int id)
        {
            var book = db.Books.Include(bk=>bk.Category).FirstOrDefault(bk => bk.BookId == id);
            return View(book);
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            var book = db.Books.Include(bk => bk.Category).FirstOrDefault(bk => bk.BookId == id);
            if (book == null)
            {
                return RedirectToAction("Index");
            }
            return View(book);
        }
        [HttpPost]
        public IActionResult Delete(int id, string confirm)
        {
            if (confirm == "yes")
            {
                var book = db.Books.Include(bk => bk.Category).FirstOrDefault(bk => bk.BookId == id);
                if (book == null)
                {
                    return RedirectToAction("Index");
                }
                db.Books.Remove(book);
                db.SaveChanges();
            }

            return RedirectToAction("Index");
        }




    }

}
