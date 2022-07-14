using FRONTTOBACK.Model;
using FRONTTOBACK.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static FRONTTOBACK.Helpers.Helper;
using SignInResult = Microsoft.AspNetCore.Identity.SignInResult;

namespace FRONTTOBACK.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _rolemanager;
        private readonly SignInManager<AppUser> _signInManager;


       public AccountController(UserManager<AppUser> userManager , RoleManager<IdentityRole> roleManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _rolemanager = roleManager;
            _signInManager = signInManager;
        }

        public IActionResult Index() 
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterVM registerVM )
        {
            if (!ModelState.IsValid) return View();

            AppUser user = new AppUser
            {
             FullName = registerVM.FullName,
             UserName = registerVM.UserName,
             Email = registerVM.Email,

        }; 
          IdentityResult result =  await _userManager.CreateAsync(user, registerVM.PassWord);

            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("" ,item.Description);
                }
                return View(registerVM);

            }
           
            await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
            return RedirectToAction("login", "account");
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async  Task<IActionResult> Login(LoginVM login , string ReturnUrl)
        {
            if (!ModelState.IsValid) return View();

            AppUser appuser =await _userManager.FindByEmailAsync(login.Email);

            if (appuser == null)
            {
                ModelState.AddModelError("", "Email or Password is invalid");
                return View(login);
            }
            SignInResult result = await _signInManager.PasswordSignInAsync(appuser, login.PassWord, true, true);

            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Accout was blocked");
                return View(login);
            }

            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Email or Password is invalid");
                return View(login);
            }
            await _signInManager.SignInAsync(appuser, true);
            var roles = await _userManager.GetRolesAsync(appuser);


            foreach (var item in roles)
            {
                if (item == "Admin")
                {
                    return RedirectToAction("index" , "dashboard" , new {area = "AdminPanel"});
                }
            }


            if (ReturnUrl != null)
            {
                return Redirect(ReturnUrl);
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("login");
        }

        public async Task CreateRole()
        {
            foreach (var item in Enum.GetValues(typeof(UserRoles)))
            {
                if (!await _rolemanager.RoleExistsAsync(item.ToString()))
                {
                    await _rolemanager.CreateAsync(new IdentityRole { Name = item.ToString() });
                }
            };
        }
    }
}
