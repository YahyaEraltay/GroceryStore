using GroceryProject.Models;
using GroceryProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GroceryProject.Controllers
{
    public class CategoryController : Controller
    {
        Context db = new Context();
        CategoryRepository categoryRepository = new CategoryRepository();
        public IActionResult Index()
        {
            return View(categoryRepository.TList());
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddCategory(Category p)
        {
            if (!ModelState.IsValid)
            {
                var messages = ModelState.ToList();
                return View("AddCategory");
            }
            categoryRepository.TAdd(p);
            return RedirectToAction("Index");
        }
        public IActionResult GetCategory(int id)
        {
            var x = categoryRepository.TGet(id);
            Category ct = new Category()
            {
                CategoryID = x.CategoryID,
                CategoryName = x.CategoryName,
                CategoryDescription = x.CategoryDescription
            };
            return View(ct);
        }
        [HttpPost]
        public IActionResult UpdateCategory(Category p)
        {
            var x = categoryRepository.TGet(p.CategoryID);
            x.CategoryName = p.CategoryName;
            x.CategoryDescription = p.CategoryDescription;
            x.CategoryStatus = true;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
        public IActionResult DeleteCategory(int id)
        {
            var x= categoryRepository.TGet(id);
            x.CategoryStatus = false;
            categoryRepository.TUpdate(x);
            return RedirectToAction("Index");
        }
    }
}
