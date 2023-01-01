using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectWEB.ViewModels;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ProjectWEB.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ShoppingCartId { get; set; }
        public List<CartItem> CartItems { get; set; }
        public string UserId { get; set; }

        public double Total(ShoppingCart cart)
        {
            double total = 0;
            foreach (var item in cart.CartItems)
            {
                total += item.Food.Price * item.Amount;
            }
            return total;
        }
    } 
}