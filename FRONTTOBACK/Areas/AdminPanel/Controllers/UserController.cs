using FRONTTOBACK.Model;
using FRONTTOBACK.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FRONTTOBACK.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class UserController : Controller
    {     
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly SignInManager<AppUser> _signInManager;


        public UserController (UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _rolemanager = roleManager;
            _signInManager = signInManager;
        }
        public IActionResult Index(string search)
        {
            var users=search==null?_userManager.Users.ToList():_userManager.Users.Where(user=>user.FullName.ToLower().Contains(search.ToLower())).ToList();
            return View(users);
        }

        public async Task<IActionResult> Update (string id)
        {

            AppUser user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _rolemanager.Roles.ToList();

            RoleVM roleVM = new RoleVM
            {
                FullName = user.FullName,
                roles = roles,
                userRoles = userRoles,
                UserId = user.Id
            };
            return View(roleVM);
        }


        [HttpPost]
        public async Task<IActionResult> Update(List <string> roles , string id)
        {
            AppUser user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);
            var addRoles = roles.Except(userRoles); // teze rollari elave etdi kohne rollari except elemeknen
            var removedRoles = userRoles.Except(roles);//kohne rollari sildi teze rollari except elemeknen 
            await _userManager.AddToRolesAsync(user , addRoles);
            await _userManager.RemoveFromRolesAsync(user, removedRoles);

            return RedirectToAction("Index");
        }
            
    
    }
}
