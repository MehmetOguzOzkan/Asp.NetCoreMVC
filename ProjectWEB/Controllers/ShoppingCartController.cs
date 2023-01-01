using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWEB.Models;
using ProjectWEB.Repositories;

namespace ProjectWEB.Controllers
{
    public class ShoppingCartController : Controller
    {
        Context context = new Context();
        IHttpContextAccessor accessor = new HttpContextAccessor();
        ShoppingCart shoppingCart;
        public IActionResult Index()
        {
            string user = accessor.HttpContext.Request.Cookies["UserId"];
            if (user == null)
            {
                user = Guid.NewGuid().ToString();
                accessor.HttpContext.Response.Cookies.Append("UserId", user);
            }
            shoppingCart = context.ShoppingCarts.FirstOrDefault(c => c.UserId == user) ?? new ShoppingCart() { UserId = user };
            shoppingCart.CartItems = context.CartItems.Where(c => c.ShoppingCartId == shoppingCart.ShoppingCartId).Include(c => c.Food).ToList();
            return View(shoppingCart);
        }
        public RedirectToActionResult AddToShoppingCart(int id)
        {
            string userId = accessor.HttpContext.Request.Cookies["UserId"];
            if (userId == null)
            {
                userId = Guid.NewGuid().ToString();
                accessor.HttpContext.Response.Cookies.Append("UserId", userId);
            }
            //var items = context.CartItems.Where(c => c.ShoppingCartId == shoppingCart.ShoppingCartId).Include(c => c.Food).ToList();
            // shoppingCart.CartItems ?? (shoppingCart.CartItems = context.CartItems.Where(x => x.ShoppingCartId == shoppingCart.ShoppingCartId).Include(c=>c.Food).ToList());
            shoppingCart = context.ShoppingCarts.FirstOrDefault(c => c.UserId == userId) ?? new ShoppingCart() { UserId = userId };
            var food = context.Foods.FirstOrDefault(s => s.FoodID == id);
            if (food is not null)
            {
                var shoppingCartItem = context.CartItems.SingleOrDefault(
                    s => s.Food.FoodID == food.FoodID && s.ShoppingCartId == shoppingCart.ShoppingCartId);
                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new CartItem
                    {
                        ShoppingCart = shoppingCart,
                        ShoppingCartId = shoppingCart.ShoppingCartId,
                        Food = food,
                        FoodID = food.FoodID,
                        Amount = 1
                    };
                    context.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Amount++;
                    context.Update(shoppingCartItem);
                }
                context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveFromShoppingCart(int id)
        {
            string userId = accessor.HttpContext.Request.Cookies["UserId"];
            if (userId == null)
            {
                userId = Guid.NewGuid().ToString();
                accessor.HttpContext.Response.Cookies.Append("UserId", userId);
            }
            shoppingCart = context.ShoppingCarts.FirstOrDefault(c => c.UserId == userId);
            var selectedCartItem = context.CartItems.FirstOrDefault(c => c.CartItemId == id);
            if (selectedCartItem is not null)
            {
                /*var localAmount = 0;
                if (selectedCartItem.Amount > 1)
                {
                    selectedCartItem.Amount--;
                    localAmount = selectedCartItem.Amount;
                }
                else
                {
                    
                }*/
                context.CartItems.Remove(selectedCartItem);
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public RedirectToActionResult IncreaseAmount(int id)
        {
            var selectedCartItem = context.CartItems.FirstOrDefault(c => c.CartItemId == id);
            if (selectedCartItem is not null)
            {
                selectedCartItem.Amount++;
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public RedirectToActionResult DecreaseAmount(int id)
        {
            var selectedCartItem = context.CartItems.FirstOrDefault(c => c.CartItemId == id);
            if (selectedCartItem is not null)
            {
                if (selectedCartItem.Amount > 0)
                {
                    selectedCartItem.Amount--;
                }
            }
            context.SaveChanges();
            return RedirectToAction("Index");
        }
        public RedirectToActionResult RemoveShoppingCart(int id)
        {
            var selectedCart = context.ShoppingCarts.FirstOrDefault(c => c.ShoppingCartId == id);
            if (selectedCart is not null)
            {
                var cartItems = context.CartItems.Where(c => c.ShoppingCartId == id).ToList();
                foreach (var item in cartItems)
                {
                    var food = context.Foods.FirstOrDefault(c=>c.FoodID==item.FoodID);
                    if (food.Stock >= item.Amount)
                    {
                        food.Stock = food.Stock - item.Amount;
                    }
                }
                context.ShoppingCarts.Remove(selectedCart);
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Default");
        }
    }
}
