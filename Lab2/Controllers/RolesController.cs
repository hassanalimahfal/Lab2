using Lab2.Models;
using Lab2.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Lab2.Controllers
{
    public class RolesController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly UserManager<SysUser> userManager;

        //نستدعي الرول مانقر
        public RolesController(RoleManager<IdentityRole> roleManager, UserManager<SysUser>  userManager  )
        {
            this.roleManager = roleManager;
            this.userManager = userManager;
        }
        public async Task<IActionResult> Index()
        { //نحتاج نستدعي الرولس الموجوده معانا 

            var rols =await roleManager.Roles.ToListAsync();
            return View(rols);
        }
        //lll
        public IActionResult Create() 
        {
            return View();
        }

        //ايش تستقبل الصفحه هذي تستقبل يوزر ايدي 

        [HttpGet]

        public async Task<IActionResult> Manage(string Id)
        {
            //اذا مافيش يوزر ايدي  او اذا مارسلش يوزر ايدي 
            if (string.IsNullOrEmpty(Id))
            {
                TempData["error"] = "Enter user Id";
                //يرجع للاندكس حق الاكاونت اليوزر
                return RedirectToAction("Index","Account");
            }
            var user = await userManager.FindByIdAsync(Id);
            if (user == null)
            {
                TempData["erorr"] = "no user found with this Id";
                return RedirectToAction("Index" , "Account");
            }

            var userRole = await userManager.GetRolesAsync(user) ;
            var roleName = userRole.FirstOrDefault();
            if(userRole != null)
            {
                ViewBag.UserRole = roleName;
            }
            //ابني حقي الفيو مودل 
            var userRoles = new UserRolesVM
            {
                UserId = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
            };
            //الاستعلام من الصلاحيات
            // الان جبنا كل الصلاحيات
            var allRoles = await roleManager.Roles.ToListAsync();
            var rolesList = new List<RoleVM>();
            foreach (var role in allRoles)
            {
                if(await userManager.IsInRoleAsync(user, role.Name))
                {
                    rolesList.Add(new RoleVM
                    {
                        Name = role.Name,
                        Id = role.Id,
                        Checked = true
                    });
                }
                else
                {
                    rolesList.Add(new RoleVM
                    {
                        Name = role.Name,
                        Id = role.Id,
                        Checked = false

                    });
                }
            }
            // الان يعتبر البيانات كله موجوده داخله الايدي نام واليوزر نام والفول نام
            userRoles.Roles = rolesList;
            //ونرسل هذا كامل للواجهه
            return View(userRoles);
        }
        [HttpPost]
        public async Task<IActionResult> Manage(UserRolesVM userRoles )
        {
            if (userRoles == null)
            {
                TempData["erorr"] = "pleass enter all data";
                return View();
            }
            //استعلم عن اليوزر حسب الايدي حقة 
            var user = await userManager.FindByIdAsync(userRoles.UserId);
            //اجيبله اسما الرولز الي اريد احذفهم فيها جميع الاسما اعمل سلكت لعمود واحد وارييد الاعمده اقله ااس متغير اشتي النام
            //داله الحذف
            foreach (var r in userRoles.Roles)
            {
                if (await userManager.IsInRoleAsync(user, r.Name))
                {
                    var del = await userManager.RemoveFromRoleAsync(user, r.Name);
                }

            }
          

            //اذا حذفت خذ الذينهن اتشك فقط وضيفهم 
          
                var checkedList =userRoles.Roles.Where(s=>s.Checked).ToList();
                //بحتاج الاسم فقط عشان ارسله  
                var addToRole = await userManager.AddToRolesAsync(user, checkedList.Select(s =>s.Name));
            
            //رجعني للاندككس حق الاكاونت
            return RedirectToAction("Index","Account");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return View();
            }
            var role =new IdentityRole { Name = name };
            var res = await roleManager.CreateAsync(role); //هذي الريزلت ترجع لنا ترو اور فولس الانتضار اويت
             if(res.Succeeded)
            {
                //رجعني الى نفس الواجهه مع البيانات RedirectToAction
                return RedirectToAction ("index");
            }
            return View(model:name);
        }
    }
}
