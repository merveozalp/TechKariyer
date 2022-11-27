using Entities.Concrete;
using Entities.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI.Controllers
{
    public class LoginController : Controller
    {
        public SignInManager<AppUser> signInManager { get; }
        private UserManager<AppUser> userManager { get; }
        public LoginController(SignInManager<AppUser> signInManager , UserManager<AppUser> userManager)
        {
            this.signInManager = signInManager;
            this.userManager= userManager;
        }

        public IActionResult SignUp()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel signUpViewModel)
        {
            if (ModelState.IsValid)
            {


                AppUser user = new AppUser();
                user.UserName = signUpViewModel.UserName;
                user.FistName = signUpViewModel.FirstName;
                user.LastName = signUpViewModel.LastName;
                user.Email = signUpViewModel.Email;
                user.Brand = signUpViewModel.Brand;
                user.BirthPlace = signUpViewModel.BirthPlace;


                IdentityResult result = await userManager.CreateAsync(user, signUpViewModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("LogIn");
                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }

            return View(signUpViewModel);
        }
        public IActionResult LogIn(string ReturnUrl)
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogIn(UserViewModel userlogin)
        {

            if (ModelState.IsValid)
            {
                AppUser user = await userManager.FindByEmailAsync(userlogin.Email);
                if (user != null)
                {
                    await signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await signInManager.PasswordSignInAsync(user,userlogin.Password,false,false);

                    //IList<string> roles = await userManager.GetRolesAsync(user);

                    //foreach (var item in roles)
                    //{
                    //    if (item.Contains("SysAdmin"))
                    //    {
                    //        return RedirectToAction("RoleCreate", "SysAdmin");
                    //    }
                    //    else if (item.Contains("Admin"))
                    //    {
                    //        return RedirectToAction("Index", "Admin");
                    //    }
                    //    else if (item.Contains("Customer"))
                    //    {
                    //        return RedirectToAction("Index", "Customer");
                    //    }

                    // }
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "SysAdmin");
                    }
                }
                else
                {
                    ModelState.AddModelError(" ", "Geçersiz Kullanıcı Adı veya Şifresi");
                }
            }
           
            return View(userlogin);







           
        }
    }
}
