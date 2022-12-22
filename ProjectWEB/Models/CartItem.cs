namespace ProjectWEB.Models
{
    public class CartItem
    {
        public int CartItemId { get; set; }
        public Food Food { get; set; }
        public int Amount { get; set; }
        public string CartId { get; set; }
    }
}
