using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjectWEB.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using ProjectWEB.Repositories;
using X.PagedList;
using ProjectWEB.Models;
using System.Linq;
using Microsoft.AspNetCore.Localization;

namespace ProjectWEB.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {
        private readonly IHttpContextAccessor accessor;
        Context context = new Context();
        FoodRepository foodRepository = new FoodRepository();
        public DefaultController(IHttpContextAccessor httpContextAccessor) 
        {
            accessor = httpContextAccessor ?? throw new ArgumentNullException();
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Search() 
        {
            var search = Request.Form["searchString"];
            var list = await context.Foods.Where(c => c.Name.Contains(search)).ToListAsync();
            return View(list);
        }
        public IActionResult AboutUs() 
        {
            return View();
        }
        public IActionResult CategoryDetails(int id) 
        {
            ViewBag.id = id;
            var category = context.Categories.FirstOrDefault(c=>c.CategoryID==id);
            return View(category);
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
        public IActionResult ChangeLanguage(string culture)
        {
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
