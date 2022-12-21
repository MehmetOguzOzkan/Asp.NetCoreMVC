using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Security.Cryptography;
using ProjectWEB.Models;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Http;

namespace ProjectWEB.Controllers
{
    public class CartController : Controller
    {
        Context c = new Context();
        Cart[] carts = new Cart[300];
        int x=0;
        public IActionResult Index()
        {
            return View(GetCart());
        }
        public IActionResult AddToCart(int id) 
        {
            var food = c.Foods.FirstOrDefault(i => i.FoodID == id);
            if (food != null) 
            {
                GetCart().AddFood(food,1);
            }
            return RedirectToAction("Index");
        }
        public Cart GetCart() 
        {
            var id = HttpContext.Session.GetInt32("Cart");
            if (id == null)
            {
                Cart cart = new Cart();
                cart.id = x;
                carts[x] = cart;
                HttpContext.Session.SetInt32("Cart", cart.id);
                x++;
                return cart;
            }
            for (int i = 0; i < 300; i++) 
            {
                if (carts[i].id == id) {
                    return carts[i];
                }
            }
            return null;
        }
        public IActionResult RemoveFromCart(int id)
        {
            var food = c.Foods.FirstOrDefault(i => i.FoodID == id);
            if (food != null)
            {
                GetCart().DeleteFood(food);
            }
            return RedirectToAction("Index");
        }
    }
}
