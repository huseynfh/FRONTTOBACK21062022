using FRONTTOBACK.DAL;
using FRONTTOBACK.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTTOBACK.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {

        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;

        }

        public IActionResult Index()
        {
            List<Category> categories = _context.Categories.ToList();
            return View(categories);
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            bool existNameCategory = _context.Categories.Any(c => c.Name.ToLower() == category.Name.ToLower());
            if (existNameCategory)
            {
                ModelState.AddModelError("Name", "This name is already exist");
                return View();
            }


            Category newCategory = new Category {
                Name = category.Name,
                Desc = category.Desc,
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Category dbcategory = await _context.Categories.FindAsync(id);
            if (dbcategory == null) return NotFound();
            return View(dbcategory);
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null) return NotFound();
            Category dbcategory = await _context.Categories.FindAsync(id);
            if (dbcategory == null) return NotFound();
            return View(dbcategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            Category dbcategory = _context.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (dbcategory == null)
            {
                return View();
            }
            Category dbcategoryName = _context.Categories.FirstOrDefault(c => c.Name.ToLower() == category.Name.ToLower());



            if (dbcategoryName != null)
            {
                if (dbcategory.Name != dbcategoryName.Name)
                {
                    ModelState.AddModelError("Name", "This name is already exist");
                    return View();
                }

            }



            dbcategory.Name = category.Name;
            dbcategory.Desc = category.Desc;
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Category dbcategory = await _context.Categories.FindAsync(id);
            if (dbcategory == null) return NotFound();
            _context.Categories.Remove(dbcategory);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

    
    }
}




