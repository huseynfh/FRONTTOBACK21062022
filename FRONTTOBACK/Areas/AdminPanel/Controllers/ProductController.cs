using FRONTTOBACK.DAL;
using FRONTTOBACK.Extentions;
using FRONTTOBACK.Model;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTTOBACK.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]

    public class ProductController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;

        public ProductController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_context.Products.Include(p => p.Category).ToList());

        }


        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null) return NotFound();
            Product dbproduct = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);
            if (dbproduct == null) return NotFound();
            return View(dbproduct);
        }

        public IActionResult Create()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "Id", "Name");
            // Product dbproduct = await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == product.Id);

            if (!ModelState.IsValid)
            {
                return View();
            }

            if (product.Photo == null)
            {
                ModelState.AddModelError("Photo", "This Photo is not exist");
                return View();
            }

            if (!product.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You can add only image type");
                return View();
            }

            if (product.Photo.IsSize(200))
            {
                ModelState.AddModelError("Photo", "You can add max size 200px");
                return View();
            }



            //string filename = Guid.NewGuid().ToString() + product.Photo.FileName;

            //string path =Path.Combine(_env.WebRootPath,"img",filename);

            //using (FileStream stream = new FileStream(path , FileMode.Create))
            //{
            //    product.Photo.CopyTo(stream);
            //};



            Product newProduct = new Product
            {
                Price = product.Price,
                Name = product.Name,
                CategoryId = product.CategoryId,
                ImageUrl = product.Photo.SaveImage(_env, ""),//"img"
            };


            await _context.Products.AddAsync(newProduct);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }





        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            Product dbproduct = await _context.Products.FindAsync(id);
            if (dbproduct == null) return NotFound();

            string path = Path.Combine(_env.WebRootPath, "", dbproduct.ImageUrl); //"img"

            Helpers.Helper.DeleteImage(path);

            _context.Products.Remove(dbproduct);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Update(int? id)
        {

            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");

            if (id == null) return NotFound();

            Product dbProduct = await _context.Products.FindAsync(id);

            if (dbProduct == null) return NotFound();

            return View(dbProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Product product)
        {
            ViewBag.Categories = new SelectList(await _context.Categories.ToListAsync(), "Id", "Name");
            if (!ModelState.IsValid)
            {
                return View();
            }
            Product dbProduct = await _context.Products.FindAsync(product.Id);
            if (dbProduct == null)
            {
                return View();
            }
            else
            {
                Product dbProductName = await _context.Products.FirstOrDefaultAsync(p => p.Name.Trim().ToLower() == product.Name.Trim().ToLower());
                if (dbProductName != null)
                {
                    if (dbProduct.Name.Trim().ToLower() != dbProductName.Name.Trim().ToLower())
                    {
                        ModelState.AddModelError("Name", "This name product is already exist");
                        return View();
                    }

                }
                if (product.Photo == null)
                {
                    dbProduct.ImageUrl = dbProduct.ImageUrl;
                }
                else
                {
                    if (!product.Photo.IsImage())
                    {
                        ModelState.AddModelError("Photo", "Choose photo please !!!");
                        return View();
                    }
                    if (product.Photo.IsSize(200))
                    {
                        ModelState.AddModelError("Photo", "Oversize");
                        return View();
                    }
                    string oldPhoto = dbProduct.ImageUrl;
                    string path = Path.Combine(_env.WebRootPath, "", oldPhoto);
                    dbProduct.ImageUrl = product.Photo.SaveImage(_env, "");
                    Helpers.Helper.DeleteImage(path);
                }

                dbProduct.Name = product.Name;
                dbProduct.Price = product.Price;
                dbProduct.Count = product.Count;
                dbProduct.CategoryId= product.CategoryId;
                await _context.SaveChangesAsync();

            }

            return RedirectToAction("Index"); 
        }
    }
}
