using GroceryProject.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryProject.Controllers
{
    public class ChartController : Controller
    {
        Context db = new Context();
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Index2()
        {
            return View();
        }
        public IActionResult VisualizeProductResult()
        {
            return Json(ProductList());
        }
        public List<Chart> ProductList()
        {
            List<Chart> cs = new List<Chart>();
            using (var c = new Context())
            {
                cs = c.Foods.Where(y => y.FoodStatus == true).Select(x => new Chart
                {
                    ProductName = x.FoodName,
                    ProductStock = x.FoodStock
                }).ToList();
            }
            return cs;
        }
        public IActionResult Statistics()
        {
            var value1 = db.Foods.Where(x => x.FoodStatus == true).Count();
            ViewBag.v1 = value1;

            var value2 = db.Foods.Where(x => x.FoodStatus == true).Sum(x => x.FoodStock);
            ViewBag.v2 = value2;

            var value3 = db.Foods.Where(z => z.FoodStatus == true).OrderByDescending(x => x.FoodStock).Select(y => y.FoodName).FirstOrDefault();
            ViewBag.v3 = value3;

            var value4 = db.Foods.Where(z => z.FoodStatus == true).OrderBy(x => x.FoodStock).Select(y => y.FoodName).FirstOrDefault();
            ViewBag.v4 = value4;

            var value5 = db.Foods.Where(x=> x.FoodStatus == true && x.CategoryID == 2).Count();
            ViewBag.v5 = value5;

            var value6 = db.Foods.Where(x => x.FoodStatus == true && x.CategoryID == 1 ).Count();
            ViewBag.v6 = value6;

            var value7 = db.Foods.Where(x => x.FoodStatus == true && x.CategoryID== 3).Count();
            ViewBag.v7 = value7;

            var value8 = db.Foods.Where(x => x.FoodStatus == true && x.CategoryID == 4).Count();
            ViewBag.v8 = value8;

            var value9 = db.Foods.Average(x => x.FoodPrice).ToString("0.00");
            ViewBag.v9 = value9;

            var value10 = db.Foods.Where(z => z.FoodStatus == true).OrderByDescending(x => x.FoodPrice).Select(y => y.FoodName).FirstOrDefault();
            ViewBag.v10 = value10;

            return View();
        }
    }
}
