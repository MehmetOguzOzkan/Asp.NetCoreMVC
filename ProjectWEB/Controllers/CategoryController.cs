using Microsoft.AspNetCore.Mvc;
using ProjectWEB.Repositories;
using ProjectWEB.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Localization;

namespace ProjectWEB.Controllers
{
    public class CategoryController : Controller
    {
        List<Category> categoryList = new List<Category>();
        CategoryRepository categoryRepository = new CategoryRepository();
        HttpClient httpClient=new HttpClient();
        Context context = new Context();
        public async Task<IActionResult> Index()
        {
            var response = await httpClient.GetAsync("https://localhost:44324/api/CategoryApi");
            string responseString=await response.Content.ReadAsStringAsync();
            categoryList=JsonConvert.DeserializeObject<List<Category>>(responseString);
            return View(categoryList);
        }
        [HttpGet]
        public IActionResult CategoryAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category p) 
        {
            p.CategoryState = true;
            //if (!ModelState.IsValid)
            //{
            //    return View("CategoryAdd");
            //}
            categoryRepository.TAdd(p);
            return RedirectToAction("Index");
        }
        public IActionResult CategoryGet(int id) 
        {
            var x = categoryRepository.TGet(id);
            Category ct = new Category()
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription,
                CategoryImage= x.CategoryImage,
            };
            return View(ct);
        }
        [HttpPost]
        public IActionResult CategoryUpdate(Category p) 
        {
            var x = categoryRepository.TGet(p.CategoryID);
            x.CategoryName = p.CategoryName;
            x.CategoryDescription = p.CategoryDescription;
            x.CategoryImage = p.CategoryImage;
            x.CategoryState = true;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
        public IActionResult CategoryDelete(int id) 
        {
            var x = categoryRepository.TGet(id);
            //x.CategoryState = false;
            categoryRepository.TDelete(x);
            return RedirectToAction("Index");
        }
    }
}