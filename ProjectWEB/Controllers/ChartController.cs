using Microsoft.AspNetCore.Mvc;
using ProjectWEB.Models;
using System.Runtime.CompilerServices;

namespace ProjectWEB.Controllers
{
    public class ChartController : Controller
    {
        public IActionResult Index() 
        {
            return View();
        }
        public IActionResult VisualizeProductResult()
        {
            //Context context = new Context();
            return Json(FoodList());
        }
        public List<Chart> FoodList() 
        {
            List<Chart> l=new List<Chart>();
            using (var c = new Context()) 
            {
                l = c.Foods.Select(x => new Chart
                {
                    foodname = x.Name,
                    stock= x.Stock,
                }).ToList();

            }
            return l;
        }
        public IActionResult Statistics()
        {
            Context c = new Context();
            var etid = c.Categories.Where(x => x.CategoryName == "Et Ürünleri").Select(y => y.CategoryID).FirstOrDefault();
            var tavukid = c.Categories.Where(x => x.CategoryName == "Tavuk Ürünleri").Select(y => y.CategoryID).FirstOrDefault();
            var denizid = c.Categories.Where(x => x.CategoryName == "Deniz Ürünleri").Select(y => y.CategoryID).FirstOrDefault();
            var sutid = c.Categories.Where(x => x.CategoryName == "Süt Ürünleri").Select(y => y.CategoryID).FirstOrDefault();
            var et = c.Foods.Where(x => x.CategoryID == etid).Count();
            ViewBag.et = et;
            var tavuk = c.Foods.Where(x => x.CategoryID == tavukid).Count();
            ViewBag.tavuk = tavuk;
            var deniz = c.Foods.Where(x => x.CategoryID == denizid).Count();
            ViewBag.deniz = deniz;
            var sut = c.Foods.Where(x => x.CategoryID == sutid).Count();
            ViewBag.sut = sut;
            var totalStock = c.Foods.Sum(x => x.Stock);
            ViewBag.totalStock = totalStock;
            var maxStockFood=c.Foods.OrderByDescending(x => x.Stock).Select(y=>y.Name).FirstOrDefault();
            ViewBag.maxStockFood = maxStockFood;
            var minStockFood = c.Foods.OrderBy(x => x.Stock).Select(y => y.Name).FirstOrDefault();
            ViewBag.minStockFood = minStockFood;
            var averagePriceFood=c.Foods.Average(x => x.Price).ToString("0.00");
            ViewBag.averagePriceFood = averagePriceFood;
            var denizStock = c.Foods.Where(y => y.CategoryID == denizid).Sum(x => x.Stock);
            ViewBag.denizStock=denizStock;
            var sutStock = c.Foods.Where(y => y.CategoryID == sutid).Sum(x => x.Stock);
            ViewBag.sutStock = sutStock;
            var etStock = c.Foods.Where(y => y.CategoryID == etid).Sum(x => x.Stock);
            ViewBag.etStock = etStock;
            var tavukStock = c.Foods.Where(y => y.CategoryID == tavukid).Sum(x => x.Stock);
            ViewBag.tavukStock = tavukStock;

            return View();
        }
    }
}
