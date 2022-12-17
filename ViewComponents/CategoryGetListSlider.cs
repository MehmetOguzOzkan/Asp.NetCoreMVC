using Microsoft.AspNetCore.Mvc;
using ProjectWEB.Repositories;

namespace ProjectWEB.ViewComponents
{
    public class CategoryGetListSlider : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            var categoryList = categoryRepository.TList();
            return View(categoryList);
        }
    }
}
