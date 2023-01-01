using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectWEB.Models;
using ProjectWEB.Repositories;

namespace ProjectWEB.ViewComponents
{
    public class FoodListById : ViewComponent
    {
        public IViewComponentResult Invoke(int id) 
        {
            Context c = new Context();
            var x = c.Set<Food>().Find(id);
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
                Description = x.Description
            };
            return View(f);
        }
    }
}
