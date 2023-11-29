using GroceryProject.Models;
using GroceryProject.Repositories;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryProject.Controllers
{
    public class FoodController : Controller
    {
        Context db = new Context();
        FoodRepository foodRepository = new FoodRepository();
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FoodController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View(foodRepository.TList("Category"));
        }
        [HttpGet]
        public IActionResult AddFood()
        {
            List<SelectListItem> values = (from x in db.Categories.Where(x => x.CategoryStatus == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.vls = values;
            return View();
        }
        [HttpPost]
        public IActionResult AddFood(Food p, IFormFile FoodImage)
        {

            if (!ModelState.IsValid)
            {
                List<SelectListItem> values = (from x in db.Categories.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = x.CategoryName,
                                                   Value = x.CategoryID.ToString()
                                               }).ToList();
                ViewBag.vls = values;

                var messages = ModelState.ToList();
                return View("AddFood");
            }

            if (FoodImage != null && FoodImage.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + FoodImage.FileName;
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", uniqueFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    FoodImage.CopyTo(stream);
                }
                p.FoodImage = uniqueFileName;
            }

            foodRepository.TAdd(p);
            return RedirectToAction("Index");
        }
        public IActionResult GetFood(int id)
        {
            List<SelectListItem> values = (from x in db.Categories.Where(x => x.CategoryStatus == true).ToList()
                                           select new SelectListItem
                                           {
                                               Text = x.CategoryName,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.vls = values;

            var y = foodRepository.TGet(id);
            Food fd = new Food()
            {
                FoodName = y.FoodName,
                FoodDescription = y.FoodDescription,
                FoodPrice = y.FoodPrice,
                FoodStock = y.FoodStock,
                FoodID = y.FoodID,
                CategoryID = y.CategoryID
            };
            return View(fd);
        }
        [HttpPost]
        public IActionResult UpdateFood(Food p, IFormFile FoodImage)
        {
            if (FoodImage != null && FoodImage.Length > 0)
            {
                string uniqueFileName = Guid.NewGuid().ToString() + "_" + FoodImage.FileName;
                string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "images", uniqueFileName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    FoodImage.CopyTo(stream);
                }

                // Yiyeceğin resim yolu
                p.FoodImage = uniqueFileName;
            }
            var x = foodRepository.TGet(p.FoodID);
            x.FoodID = p.FoodID;
            x.FoodName = p.FoodName;
            x.FoodDescription = p.FoodDescription;
            x.FoodImage = p.FoodImage;
            x.FoodPrice = p.FoodPrice;
            x.FoodStock = p.FoodStock;
            x.CategoryID = p.CategoryID;
            foodRepository.TUpdate(x);

            return RedirectToAction("Index");
        }
        public IActionResult DeleteFood(int id)
        {
            var x = foodRepository.TGet(id);
            x.FoodStatus = false;
            foodRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
    }
}
