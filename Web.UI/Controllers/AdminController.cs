using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.UI.Controllers
{
    //[Authorize(Roles = "Admin,SystemAdmin")]
    public class AdminController : Controller
    {

        public IActionResult Index()
        {
            //List<ProductListDto> product = new List<ProductListDto>();
            //using (var httpClient = new HttpClient())
            //{
            //    using var response = await httpClient.GetAsync(""))
            //        {

            //    }
            //}
            return View();
        }
    }
}
