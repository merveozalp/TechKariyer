using Business.Abstract;
using Entities.Concrete;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI.Controllers
{
    
    public class SysAdminController : Controller
    {

        private UserManager<AppUser> userManager { get; }
        private RoleManager<AppRole> roleManager { get; }
        //private IProductService productService;

        public SysAdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            //this.productService=productService;

        }
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {

                ModelState.AddModelError("", item.Description);

            }

        }
        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public IActionResult RoleCreate()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RoleCreate(RoleViewModel roles)
        {
            AppRole role = new AppRole();
            role.Name = roles.Name;
            IdentityResult result = roleManager.CreateAsync(role).Result;
            if (result.Succeeded)
            {

                return RedirectToAction("Index");
            }
            else
            {
                AddModelError(result);
            }

            return View(roles);
          
        }

        public IActionResult RoleAssign(string id)
        {
            TempData["userId"] = id;
            AppUser user = userManager.FindByIdAsync(id).Result;
            ViewBag.userName = user.UserName;
            IQueryable<AppRole> roles = roleManager.Roles;
            List<string> userRoles = userManager.GetRolesAsync(user).Result as List<string>; // Hangi rollere sahipse görüyoruz.
            List<RoleAssignViewModel> roleAssignVm = new List<RoleAssignViewModel>();
            foreach (AppRole role in roles)
            {
                RoleAssignViewModel r = new RoleAssignViewModel();
                r.RoleId = role.Id;
                r.RoleName = role.Name;
                if (userRoles.Contains(role.Name))
                {
                    r.Exist = true;
                }
                else
                {
                    r.Exist = false;
                }
                roleAssignVm.Add(r);

            }
            return View(roleAssignVm);

        }
        [HttpPost]
        public async Task<IActionResult> RoleAssign(List<RoleAssignViewModel> roleAssignVm)
        {
            AppUser user = await userManager.FindByIdAsync(TempData["userId"].ToString());
            foreach (var item in roleAssignVm)
            {
                if (item.Exist)
                {
                    await userManager.AddToRoleAsync(user, item.RoleName);
                }
                else
                {
                    await userManager.RemoveFromRoleAsync(user, item.RoleName);
                }
            }
            return RedirectToAction("Index");
        }

        //public IActionResult GetAllProduct()
        //{
        //    var ListProduct=productService.GetList();
        //    return View(ListProduct);
        //}
    }
}
