using System.ComponentModel.DataAnnotations;

namespace ProjeDenemesi1.Models
{
    public class Category
    {

        public int CategoryID { get; set; }
        [Display(Name ="Kategori")]
        [Required(ErrorMessage ="Kategori adı boş geçilemez.")]
        [StringLength(30,ErrorMessage ="Lütfen 30 karakterden fazla veri girişi yapmayın.")]
        public string CategoryName { get; set; }
        [Display(Name ="Kategori Açıklaması")]
        public string CategoryDescription { get; set; }
        [Display(Name ="Durum")]
        public bool CategoryState { get; set; }
        public List<Food> Foods { get; set; }
    }
}
