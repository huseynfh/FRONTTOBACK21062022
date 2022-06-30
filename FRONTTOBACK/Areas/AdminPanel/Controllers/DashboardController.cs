using Microsoft.AspNetCore.Mvc;

namespace FRONTTOBACK.Areas.AdminPanel.Controllers
{
    public class DashboardController : Controller
    {
        [Area ("AdminPanel")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
