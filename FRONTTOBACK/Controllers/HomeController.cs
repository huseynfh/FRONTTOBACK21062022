using FRONTTOBACK.DAL;
using FRONTTOBACK.Model;
using FRONTTOBACK.Services;
using FRONTTOBACK.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FRONTTOBACK.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _context;

        private readonly ISum _sum;

        public HomeController(AppDbContext context,ISum sum)
        {
            _context = context;
            _sum = sum; 
        }
        public IActionResult Index()
        {
            HomeVM homeVm = new HomeVM();
            homeVm.Slider = _context.Slider.ToList();
            homeVm.SliderContent = _context.SliderContent.FirstOrDefault();
            homeVm.Categories = _context.Categories.ToList();
            homeVm.Products = _context.Products.Take(8).Include(p => p.Category).ToList();
            int result = _sum.Sum(4,5);
            return View(homeVm);

        }
        //public IActionResult Detail(int? id, string name)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }
        //    Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);
        //    if (dbProduct == null) return NotFound();

        //    return View(dbProduct);
        //}

       public IActionResult SearchProduct(string search)
        {
    List<Product> products = _context.Products.Include(p => p.Category).OrderBy(p => p.Id).Where(p => p.Name.ToLower().Contains(search.ToLower())).Take(10).ToList();
     return PartialView("_SearchPartial",products);
        }
    }
}
