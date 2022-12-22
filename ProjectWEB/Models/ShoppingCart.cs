using Microsoft.EntityFrameworkCore;
using ProjectWEB.ViewModels;

namespace ProjectWEB.Models
{
    public class ShoppingCart
    {
        Context _context = new Context();
        private ShoppingCart(Context context)
        {
            _context = context;
        }
        public string ShoppingCartId { get; set; }
        public List<CartItem> CartItems { get; set; }

        public static ShoppingCart GetCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            var context = services.GetService<Context>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

            session.SetString("CartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddCart(Food food, int amount)
        {
            var shoppingCartItem =
                _context.CartItems.FirstOrDefault(
                    s => s.Food.FoodID == food.FoodID && s.CartId == ShoppingCartId);
            if (shoppingCartItem == null)
            {
                shoppingCartItem = new CartItem
                {
                    CartId = ShoppingCartId,
                    Food = food,
                    Amount = 1
                };
            }
            else
            {
                shoppingCartItem.Amount++;
            }
            _context.SaveChanges();
        }
        public int RemoveFromCart(Food food) {
            var shoppingCartItem =
                _context.CartItems.SingleOrDefault(
                    s => s.Food.FoodID == food.FoodID && s.CartId == ShoppingCartId);

            var localAmount = 0;
            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                    localAmount = shoppingCartItem.Amount;
                }
                else 
                {
                    _context.CartItems.Remove(shoppingCartItem);
                }
            }
            _context.SaveChanges();
            return localAmount;
        }
        public List<CartItem> GetCartItems() 
        {
            return CartItems ?? (CartItems =
                _context.CartItems.Where(c => c.CartId==ShoppingCartId).Include(s => s.Food).ToList());
        }
        public void ClearCart() 
        {
            var cartItems=_context.CartItems.Where(
                cart=>cart.CartId==ShoppingCartId);
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();
        }
        public double GetShoppingCartTotal() 
        {
            var total = _context.CartItems.Where(
                c => c.CartId == ShoppingCartId).Select(
                c => c.Food.Price * c.Amount).Sum();
            
            return total;
        }

    }
}