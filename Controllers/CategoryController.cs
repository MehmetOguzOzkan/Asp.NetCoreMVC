using Microsoft.AspNetCore.Mvc;
using ProjeDenemesi1.Repositories;
using ProjeDenemesi1.Models;

namespace ProjeDenemesi1.Controllers
{
    public class CategoryController : Controller
    {
        CategoryRepository categoryRepository = new CategoryRepository();

        public IActionResult Index()
        {
            return View(categoryRepository.TList());
        }
        [HttpGet]
        public IActionResult CategoryAdd() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult CategoryAdd(Category p) 
        {
            if(!ModelState.IsValid) 
            {
                return View("CategoryAdd");
            }
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
            };
            return View(ct);
        }
        [HttpPost]
        public IActionResult CategoryUpdate(Category p) 
        {
            var x = categoryRepository.TGet(p.CategoryID);
            x.CategoryName = p.CategoryName;
            x.CategoryDescription = p.CategoryDescription;
            x.CategoryState = true;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
        public IActionResult CategoryDelete(int id) 
        {
            var x = categoryRepository.TGet(id);
            x.CategoryState = false;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
    }
}