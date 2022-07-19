using FRONTTOBACK.DAL;
using FRONTTOBACK.Model;
using FRONTTOBACK.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTTOBACK.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;

        public BasketController(AppDbContext context , UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> AddItem(int? id)
        {
            // HttpContext.Session.SetString("name","Baku");
          

            if (id == null) return NotFound();

            Product dbProduct = await _context.Products.FindAsync(id);

            if (dbProduct == null) return NotFound();

            List<BasketVM> products;

            if (Request.Cookies["basket"] == null)
            {
                products = new List<BasketVM>();
            }
            else
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>((Request.Cookies["basket"]));
            }

            BasketVM existProduct = products.Find(x => x.Id == id);

            if (existProduct == null) 
            {             
                BasketVM basketVM = new BasketVM
                {
                    Id = dbProduct.Id,
                    ProductCount = 1
                };
                products.Add(basketVM);
            }
            else
            {
                existProduct.ProductCount++;
            }


            Response.Cookies.Append("basket" , JsonConvert.SerializeObject(products) , new CookieOptions { MaxAge=TimeSpan.FromDays(14) } );

            return RedirectToAction("index","home");
        }




        public IActionResult ShowItem()
        {
            //  string name = HttpContext.Session.GetString("name");
            string basket = Request.Cookies["basket"];

            List<BasketVM> products;


            if (basket!=null)
            {
                products = JsonConvert.DeserializeObject<List<BasketVM>>(basket);
                foreach (var item in products)
                {
                    Product dbProduct = _context.Products.FirstOrDefault(p => p.Id == item.Id);
                    item.Name = dbProduct.Name;
                    item.Price = dbProduct.Price;
                    item.ImageUrl = dbProduct.ImageUrl;
                    // item.CategoryId = dbProduct.CategoryId;
                }
            }
            else
            {
                products=new List<BasketVM>();
            }


              return View(products);
        }

        [HttpPost]
        public async Task<IActionResult> Sale()
        {
           

            if (User.Identity.IsAuthenticated)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
                Sale sale = new Sale();
                sale.SaleDate = DateTime.Now;
                sale.AppUserId = user.Id;
                // sale.Total = ;


                List<BasketVM> basketProducts = JsonConvert.DeserializeObject<List<BasketVM>>(Request.Cookies["basket"]);
                List<SalesProduct> salesProducts = new List<SalesProduct>();
                double total = 0;
                foreach (var basketProduct in basketProducts)
                {
                    Product dbProduct = await _context.Products.FindAsync(basketProduct.Id);
                    if (basketProduct.ProductCount > dbProduct.Count)
                    {
                        TempData["fail"] = "Sale is fail";
                        return RedirectToAction("Showitem");
                    }

                  
                    SalesProduct salesProduct = new SalesProduct();
                    salesProduct.ProductId = dbProduct.Id;
                    salesProduct.Count = basketProduct.ProductCount;                 
                    salesProduct.SaleId = sale.Id;
                    salesProduct.Price = dbProduct.Price;
                    salesProducts.Add(salesProduct);
                    total = total + basketProduct.ProductCount * dbProduct.Price;
                }

                sale.SalesProducts = salesProducts;
                sale.Total = total;
                await _context.AddAsync(sale);
                await _context.SaveChangesAsync();
                TempData["succsess"] = "Sale is succsess";
                return RedirectToAction("Showitem");
            }
            else
            {
                return RedirectToAction("login", "account");
            }
          
            
        }
        
    }
}
