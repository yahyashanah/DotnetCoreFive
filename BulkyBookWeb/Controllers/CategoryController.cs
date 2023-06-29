using BulkyBookWeb.Data;
using BulkyBookWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            IEnumerable<Category> objCategory = _context.categories.ToList();
             return View(objCategory);
        }

        // Get
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayORder Cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _context.categories.Add(obj);
                _context.SaveChanges();
                TempData["success"] = "Category created successfully";
                return RedirectToAction("Index");
            }

           return View(obj);
        }

        // Edit
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0) 
            {
                return NotFound();
            }
            var categoryFromDb = _context.categories.Find(id);
            //var categoryFromDbFirst = _context.categories.FirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _context.categories.SingleOrDefault(x => x.Id == id);

            if(categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The DisplayORder Cannot exactly match the Name");
            }
            if (ModelState.IsValid)
            {
                _context.categories.Update(obj);
                _context.SaveChanges();
                TempData["success"] = "Category updated successfully";
                return RedirectToAction("Index");
            }

            return View(obj);
        }

        // Remove
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDb = _context.categories.Find(id);
            //var categoryFromDbFirst = _context.categories.FirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _context.categories.SingleOrDefault(x => x.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _context.categories.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            else
            {
                _context.categories.Remove(obj);
                _context.SaveChanges();
                TempData["success"] = "Category deleted successfully";
                return RedirectToAction("Index");
            }
        }


    }
}
