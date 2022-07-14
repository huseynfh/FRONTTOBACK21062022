using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FRONTTOBACK.Areas.AdminPanel.Controllers
{
    public class DashboardController : Controller
    {
        [Area ("AdminPanel")]
        [Authorize(Roles = "Admin")]

        public IActionResult Index()
        {
            return View();
        }
    }
}
