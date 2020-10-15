using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Build_Completed_E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Build_Completed_E_Commerce.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("admin")]
    [Route("admin/category")]
    public class CategoryController : Controller
    {
        private CompanyContext db = new CompanyContext();

        public CategoryController(CompanyContext _db)
        {
            db = _db;
        }

        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            ViewBag.categories = db.Categories.Where(c=> c.Parent ==null).ToList();
            return View();
        }
        [HttpGet]//có thể là Post
        [Route("add")]
        public IActionResult Add()
        {
            var category = new Category();
            return View("Add",category);
        }

        [HttpPost]//có thể là Post
        [Route("add")]
        public IActionResult Add(Category category)
        {
            category.Parent = null;
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "admin" });
        }

        [HttpGet]
        [Route("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "admin" });
        }

        [HttpGet]
        [Route("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            return View("Edit", category);
        }

        [HttpPost]
        [Route("edit/{id}")]
        public IActionResult Edit(int id,Category category)
        {
            var CurrentCategory = db.Categories.Find(id);
            CurrentCategory.Name = category.Name;
            CurrentCategory.Status = category.Status;
            db.SaveChanges();
            return RedirectToAction("Index", "category", new { area = "admin" });
        }
    }
}
