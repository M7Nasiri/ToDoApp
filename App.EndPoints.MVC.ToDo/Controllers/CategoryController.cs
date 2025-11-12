using App.Domain.Core.CategoryAgg.Contracts.AppServices;
using App.Domain.Core.CategoryAgg.Contracts.Services;
using App.Domain.Core.CategoryAgg.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.ToDo.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryAppService _catAppService;
        public CategoryController(ICategoryAppService catAppService)
        {
            _catAppService = catAppService;
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
            _catAppService.Create(category);
            return RedirectToAction("Index", "User");
        }

        public IActionResult Edit(int id)
        {
            var model = _catAppService.Get(id);
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
            _catAppService.Update(model.Id, model);
            return RedirectToAction("Index", "User");
        }
        public IActionResult Delete(int id)
        {
            var cat = _catAppService.Get(id);
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
            _catAppService.Delete(model.Id, model);
            return RedirectToAction("Index", "User");
        }

    }
}
