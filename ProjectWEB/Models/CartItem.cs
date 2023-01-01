using System.ComponentModel.DataAnnotations;

namespace ProjectWEB.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }
        public int FoodID { get; set; }
        public virtual Food Food { get; set; }
        public int Amount { get; set; }
        public int ShoppingCartId { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
    }
}
