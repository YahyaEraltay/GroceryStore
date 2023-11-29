using GroceryProject.Models;
using GroceryProject.Repositories;
using GroceryProject.ViewComponents;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryProject.Controllers
{
    [AllowAnonymous]
    public class SiteController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CategoryDetails(int id)
        {
            CategoryRepository categoryRepository = new CategoryRepository();
            var value = categoryRepository.TGet(id);
            Category ct = new Category()
            {
                CategoryName = value.CategoryName
            };
            ViewBag.vls = ct;
            ViewBag.x = id;
            return View();
        }
        [HttpGet]
        public IActionResult Checkout()
        {

            return View();
        }

        [HttpPost]

        public IActionResult Checkout(IFormCollection form)
        {
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            var FoodItems = new List<(string name, string amount, int quantity)>();

            int i = 1;
            while (true)
            {
                string nameKey = $"item_name_{i}";
                string amountKey = $"amount_{i}";
                string quantityKey = $"quantity_{i}";
                if (form.ContainsKey(nameKey) && form.ContainsKey(amountKey) && form.ContainsKey(quantityKey))
                {
                    FoodItems.Add((form[nameKey], (form[amountKey]), int.Parse(form[quantityKey])));
                    i++;
                }
                else
                {
                    break;
                }
            }

            //ViewBag.FoodItems = FoodItems;
            checkoutViewModel.FoodItems = FoodItems;

            return View(checkoutViewModel);
        }

        //// form'dan gelen tüm anahtar-değer çiftlerini al
        //           foreach (var key in form.Keys)
        //           {
        //               // item_name ile başlayan anahtarları kontrol et
        //               if (key.StartsWith("item_name"))
        //               {
        //                   // item_name için numara al
        //                   var itemNumber = key.Substring("item_name".Length);

        //                   // amount anahtarını oluştur
        //                   var amountKey = "amount" + itemNumber;

        //                   // amount anahtarının varlığını kontrol et
        //                   if (form.ContainsKey(amountKey))
        //                   {
        //                       // item_name ve amount değerlerini al
        //                       ViewBag.itemName = form[key];
        //                       ViewBag.amountValue = form[amountKey];

        //                   }
        //               }
        //           }

        // Diğer işlemleri yapabilir veya bir view'e yönlendirebilirsiniz






    }
}
