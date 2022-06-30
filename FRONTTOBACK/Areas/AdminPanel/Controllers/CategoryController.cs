using Microsoft.AspNetCore.Mvc;

namespace FRONTTOBACK.Areas.AdminPanel.Controllers
{
    public class CategoryController : Controller
    {
        [Area("AdminPanel")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
