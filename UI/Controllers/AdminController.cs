using Entities.Concrete;
using Entities.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace UI.Controllers
{
    //[Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private UserManager<AppUser> userManager { get; }
        private RoleManager<AppRole> roleManager { get; }

        public AdminController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }
        public void AddModelError(IdentityResult result)
        {
            foreach (var item in result.Errors)
            {

                ModelState.AddModelError("", item.Description);

            }

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
        public async Task<IActionResult> GetAll()
        {

            List<Product>  products = new List<Product>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7071/api/Product"))
                {
                    string apiResponse=await response.Content.ReadAsStringAsync();
                    products=JsonConvert.DeserializeObject<List<Product>>(apiResponse);
                }
            }
            return View(products);
        }

        [HttpGet]
        public async Task<IActionResult> AddProduct()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct(Product gelenProduct)
        {
            

            Product addProduct = new Product();
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(gelenProduct), Encoding.UTF8,"application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7071/api/Product/AddProduct",content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    addProduct = JsonConvert.DeserializeObject<Product>(apiResponse);
                }
            }

           // return View(addProduct);
            return RedirectToAction("GetAll");
        }
    }
}
