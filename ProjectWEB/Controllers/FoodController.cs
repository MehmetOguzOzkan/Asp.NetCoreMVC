using Microsoft.AspNetCore.Mvc;
using ProjectWEB.Repositories;
using ProjectWEB.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;
using X.PagedList.Mvc.Core;

namespace ProjectWEB.Controllers
{
    public class FoodController : Controller
    {
        Context c = new Context();
        FoodRepository foodRepository = new FoodRepository();
        public IActionResult Index(int page=1)
        {
            return View(foodRepository.TList("Category").ToPagedList(page,10));
        }
        [HttpGet]
        public IActionResult FoodAdd() 
        {
            List<SelectListItem> values = (from x in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            return View();
        }
        [HttpPost]
        public IActionResult FoodAdd(Food p)
        {
            foodRepository.TAdd(p);
            return RedirectToAction("Index"); 
        }
        public IActionResult FoodDelete(int id) 
        {

            foodRepository.TDelete(new Food { FoodID=id});
            return RedirectToAction("Index");
        }
        public IActionResult FoodGet(int id) 
        {
            var x=foodRepository.TGet(id);
            List<SelectListItem> values = (from y in c.Categories.ToList()
                                           select new SelectListItem
                                           {
                                               Text = y.CategoryName,
                                               Value = y.CategoryID.ToString()
                                           }).ToList();
            ViewBag.v1 = values;
            Food f = new Food()
            {
                FoodID = x.FoodID,
                Name = x.Name,
                Stock = x.Stock,
                Price = x.Price,
                ImageURL = x.ImageURL,
                CategoryID = x.CategoryID,                
                Description=x.Description
            };
            return View(f);
        }

        public IActionResult FoodUpdate(Food p) 
        {
            var x = c.Foods.FirstOrDefault(c=>c.FoodID==p.FoodID);
            x.Name= p.Name;
            x.Stock= p.Stock;
            x.Price= p.Price;
            x.ImageURL= p.ImageURL;
            x.Description= p.Description;
            x.CategoryID= p.CategoryID;
            foodRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
    }
}
