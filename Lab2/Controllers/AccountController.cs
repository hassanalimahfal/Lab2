using Lab2.Models;
using Lab2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{

    public class AccountController : Controller
    {
        private readonly UserManager<SysUser> userManager;
        private readonly SignInManager<SysUser> signInManager;
        private readonly RoleManager<IdentityRole> roleManager;


        public AccountController(UserManager<SysUser> userManager, SignInManager<SysUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.roleManager = roleManager;
        }

        //نعمل الاندكس الي باتعرض لي كل اليوزرات
        public async Task<IActionResult> Index()
        {
            var users = await userManager.Users.ToListAsync();
            List<UserVM> usersVMs = new List<UserVM>();
            foreach (var u in users)
            {
                usersVMs.Add(new UserVM
                { 
                    Id = u.Id,
                    Address = u.Address,
                    FullName = u.FullName,
                    UserName = u.UserName,
                    
                });

            }

            return View(usersVMs);
        }
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginVM obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(obj);
                }

                var res = await signInManager.PasswordSignInAsync(obj.UserName, obj.Password, obj.RememeberMe, false);
                if (res.Succeeded)
                {
                    Console.WriteLine("===== Login Success");
                    return RedirectToAction("Index", "Student");   
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"Username or password is not correct");
                    return View(obj);
                }
            }
            catch (Exception ex)
            {
                return View(obj);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserVM obj)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(obj);
                }

                var user = new SysUser
                {
                    FullName = obj.FullName,
                    UserName = obj.UserName,
                    Address = obj.Address
                };

                var res = await userManager.CreateAsync(user, obj.Password);
                if (res.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "User");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Error in creating account");
                    return View(obj);
                }

            }
            catch (Exception ex)
            {
                return View(obj);
            }
        }

        public async Task<IActionResult> Logout()
        {
           await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Student");   
         //   return RedirectToAction("Login", "Account");   
        }
    }
}
