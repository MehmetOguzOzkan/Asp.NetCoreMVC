using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjectWEB.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryDetails(int id) 
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult FoodDetails(int id) 
        {
            ViewBag.id = id;
            return View();
        }
        public IActionResult Contact() 
        {
            return View();
        }
    }
}
