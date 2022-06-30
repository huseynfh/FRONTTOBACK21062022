using FRONTTOBACK.DAL;
using FRONTTOBACK.Model;
using FRONTTOBACK.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FRONTTOBACK.Controllers
{
    public class ProductController : Controller
    {


        private readonly AppDbContext _context;

        public ProductController(AppDbContext context)
        {
            _context = context;

        }

            public IActionResult Index()
        {
            List<Product> products = _context.Products.Take(4).Include(p =>p.Category).ToList();
            ViewBag.ProductCount = _context.Products.Count(); //  SQL-deki datanin sayini qaytarir
            return View(products);
        }
        public IActionResult LoadMore(int skip)
        {


            //List<ProductReturnVM> products = _context.Products.Select(p => new ProductReturnVM
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Price = p.Price,
            //    CategoryId = p.CategoryId,
            //    Category = p.Category.Name,
            //    ImageUrl= p.ImageUrl
            //}).ToList();
           
            List <Product> products = _context.Products.Skip(skip).Take(4).Include(p => p.Category).ToList();

            return PartialView("_LoadMorePartial",products);
        }

        public IActionResult Detail(int? id)
        {
            
            if (id == null) return NotFound();                        
            Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == id);
            if (dbProduct == null) return NotFound();
            return View(dbProduct);
        }
    }
}
