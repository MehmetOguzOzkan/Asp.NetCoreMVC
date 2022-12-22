using Microsoft.AspNetCore.Mvc;
using ProjectWEB.Models;
using ProjectWEB.Repositories;
using ProjectWEB.ViewModels;

namespace ProjectWEB.Controllers
{
    public class ShoppingCartController : Controller
    {
        FoodRepository _foodRepository=new FoodRepository();
        ShoppingCart _shoppingCart;
        public ShoppingCartController(ShoppingCart shoppingCart) 
        {
            _shoppingCart=shoppingCart;
        }
        public ViewResult Index()
        {
            var items = _shoppingCart.GetCartItems();
            _shoppingCart.CartItems = items;

            var scvm = new ShoppingCartViewModel {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCartTotal()
            };
            return View(scvm);
        }
        public RedirectToActionResult AddToShoppingCart(int foodId) {
            var selectedFood = _foodRepository.TGet(foodId);
            if (selectedFood is not null) {
                _shoppingCart.AddCart(selectedFood, 1);
            }
            return RedirectToAction("Index");

        }
        public RedirectToActionResult RemoveFromShoppingCart(int foodId) {
            var selectedFood= _foodRepository.TGet(foodId);
            if (selectedFood is not null) 
            {
                _shoppingCart.RemoveFromCart(selectedFood);
            }
            return RedirectToAction("Index");
        }
    }
}
