using Microsoft.AspNetCore.Mvc;
using ProjectWEB.Repositories;

namespace ProjectWEB.ViewComponents
{
    public class FoodListByCategory : ViewComponent
    {
        public IViewComponentResult Invoke(int id)
        {
            FoodRepository foodRepository = new FoodRepository();
            var foodList = foodRepository.List(x=>x.CategoryID==id);
            return View(foodList);
        }
    }
}
