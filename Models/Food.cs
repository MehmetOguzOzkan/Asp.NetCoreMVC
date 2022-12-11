using System.ComponentModel.DataAnnotations;

namespace ProjeDenemesi1.Models
{
    public class Food
    {
        public int FoodID { get; set; }
        [Display(Name="Yiyecek")]
        public string Name { get; set; }
        [Display(Name = "Açıklama")]
        public string Description { get; set; }
        [Display(Name = "Fiyat")]
        public double Price { get; set; }
        [Display(Name = "Resim Linki")]
        public string ImageURL { get; set; }
        [Display(Name = "Stok Miktarı")]
        public int Stock { get; set; }
        [Display(Name = "Kategori")]
        public int CategoryID { get; set; }
        [Display(Name = "Kategori")]
        public virtual Category Category { get; set; } 
    }
}
