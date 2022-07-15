using FRONTTOBACK.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTTOBACK.Areas.AdminPanel.Controllers
{

    [Area("AdminPanel")]
    public class RoleController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly SignInManager<AppUser> _signInManager;


        public RoleController(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _rolemanager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View(_rolemanager.Roles.ToList());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
       
        public async Task<IActionResult> Create(string role)
        {
            var result = await _rolemanager.CreateAsync(new IdentityRole { Name = role });
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Delete(string id)
        {
            var result = await _rolemanager.FindByIdAsync(id);
            await _rolemanager.DeleteAsync(result);
            return RedirectToAction("Index");
        }
    }
}
