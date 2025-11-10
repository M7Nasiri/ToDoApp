using App.Domain.Core.CategoryAgg.Contracts.Services;
using App.Domain.Core.CategoryAgg.Entities;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.ToDo.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _catService;
        public CategoryController(ICategoryService catService)
        {
            _catService = catService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            _catService.Create(category);
            return RedirectToAction("Index", "User");
        }

        public IActionResult Edit(int id)
        {
            var model = _catService.Get(id);
            if (model == null)
            {
                return View();
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            _catService.Update(model.Id, model);
            return RedirectToAction("Index", "User");
        }
        public IActionResult Delete(int id)
        {
            var cat = _catService.Get(id);
            if (cat == null)
            {
                ViewBag.Error = "آیدی نامعتبر است .";
                return View();
            }
            return View(cat);
        }
        [HttpPost]
        public IActionResult Delete(Category model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _catService.Delete(model.Id, model);
            return RedirectToAction("Index", "User");
        }

    }
}
