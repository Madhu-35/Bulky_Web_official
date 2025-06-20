using Bulky_Web.Data;
using Bulky_Web.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Bulky_Web.Controllers
{
    public class CategoryController : Controller
    {
        // Injecting the ApplicationDbContext to access the database
        private readonly ApplicationDbContext _db;
        // Constructor to inject the ApplicationDbContext
        // This allows us to use dependency injection to access the database context
        public CategoryController(ApplicationDbContext db) 
        {
            _db = db;
        }
        public IActionResult Index()
        {
            // Fetching all categories from the database and converting them to a list
            // The ToList() method is used to execute the query and retrieve the results
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            // This action method returns the view for creating a new category
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if(obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj); // Adding the new category object to the database context
                _db.SaveChanges(); // Saving changes to the database

                TempData["success"] = "Category created successfully"; // Setting a success message in TempData
                return RedirectToAction("Index");
            }
            
            return View(obj); 
        }


        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound(); 
            }
            Category? categoryFromDb = _db.Categories.Find(id); // Finding the category by id in the database
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

           
            if (categoryFromDb == null)
            {
                return NotFound(); // If the category is not found, return NotFound result
            }
            return View(categoryFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
           
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj); // Adding the new category object to the database context
                _db.SaveChanges(); // Saving changes to the database
                TempData["success"] = "Category updated successfully";

                return RedirectToAction("Index");
            }

            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _db.Categories.Find(id); // Finding the category by id in the database
            //Category? categoryFromDb1 = _db.Categories.FirstOrDefault(u=>u.Id==id);
            //Category? categoryFromDb2 = _db.Categories.Where(u=>u.Id==id).FirstOrDefault();

            if (categoryFromDb == null)
            {
                return NotFound(); // If the category is not found, return NotFound result
            }
            return View(categoryFromDb);
        }


        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges(); // Saving changes to the database

            TempData["success"] = "Category deleted successfully";

            return RedirectToAction("Index");
            
        }
    }
}
