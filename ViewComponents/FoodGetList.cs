using Microsoft.AspNetCore.Mvc;
using ProjectWEB.Repositories;

namespace ProjectWEB.ViewComponents
{
    public class FoodGetList : ViewComponent
    {
        public IViewComponentResult Invoke() 
        {
            FoodRepository foodRepository=new FoodRepository();
            var foodList = foodRepository.TList();
            return View(foodList);
        }
    }
}
