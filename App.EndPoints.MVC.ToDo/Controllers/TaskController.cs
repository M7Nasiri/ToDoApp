using App.Domain.Core.CategoryAgg.Contracts.Services;
using App.Domain.Core.CategoryAgg.Entities;
using App.Domain.Core.TaskAgg.Contracts.Services;
using App.Domain.Core.TaskAgg.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace App.EndPoints.MVC.ToDo.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly ICategoryService _catService;

        public TaskController(ITaskService taskService, ICategoryService catService)
        {
            _taskService = taskService;
            _catService = catService;
        }

        public IActionResult Create()
        {
            ViewBag.Cats = new SelectList(_catService.GetAll(),"Id","Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddTaskDto dto)
        {
            //dto.CreateAt = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewBag.Cats = new SelectList(_catService.GetAll(), "Id", "Title");
                return View(dto);
            }
            dto.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = _taskService.Add(dto);
            return RedirectToAction("Index", "User");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Cats = new SelectList(_catService.GetAll(), "Id", "Title");
            var task = _taskService.GetById(id);
            if (task.Data == null)
            {
                return View();
            }
            var dto = new UpdateTaskDto
            {
                Id = id,
                CategoryId = task.Data.CategoryId,
                Description = task.Data.Description,
                DueDate = task.Data.DueDate,
                Status = task.Data.Status,
                Title = task.Data.Title,
            };
            return View(dto);
        }
        [HttpPost]
        public IActionResult Edit(UpdateTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cats = new SelectList(_catService.GetAll(), "Id", "Title");
                return View();
            }
            _taskService.Update(dto,dto.Id);
            return RedirectToAction("Index", "User");
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Cats = new SelectList(_catService.GetAll(), "Id", "Title");
            var task = _taskService.GetById(id);
            if (task.Data == null)
            {
                ViewBag.Error = "آیدی نامعتبر است .";
                return View();
            }
            var dto = new DeleteTaskDto
            {
                Id = task.Data.Id,
                CategoryId = task.Data.CategoryId,
                Description = task.Data.Description,
                DueDate = task.Data.DueDate,
                Status = task.Data.Status,
                Title = task.Data.Title,
            };
            return View(dto);
        }
        [HttpPost]
        public IActionResult Delete(DeleteTaskDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Cats = new SelectList(_catService.GetAll(), "Id", "Title");
                return View(dto);
            }
            _taskService.Delete(dto,dto.Id);
            return RedirectToAction("Index", "User");
        }
    }
}
