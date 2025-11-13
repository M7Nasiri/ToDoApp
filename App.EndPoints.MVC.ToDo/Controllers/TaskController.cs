using App.Domain.Core.CategoryAgg.Contracts.AppServices;
using App.Domain.Core.CategoryAgg.Contracts.Services;
using App.Domain.Core.CategoryAgg.Entities;
using App.Domain.Core.TaskAgg.Contracts.AppServices;
using App.Domain.Core.TaskAgg.Contracts.Repositories;
using App.Domain.Core.TaskAgg.Contracts.Services;
using App.Domain.Core.TaskAgg.DTOs;
using App.Domain.Core.UserAgg.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace App.EndPoints.MVC.ToDo.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskAppService _taskAppService;
        private readonly ICategoryAppService _catAppService;
        private readonly ITaskRepository _taskRepo;

        public TaskController(ITaskAppService taskAppService, ICategoryAppService catAppService, ITaskRepository taskRepo)
        {
            _taskAppService = taskAppService;
            _catAppService = catAppService;
            _taskRepo = taskRepo;
        }

        public IActionResult Create()
        {
            ViewBag.Cats = new SelectList(_catAppService.GetAll(),"Id","Title");
            return View();
        }

        [HttpPost]
        public IActionResult Create(AddTaskDto dto)
        {
            //dto.CreateAt = DateTime.Now;
            if (!ModelState.IsValid)
            {
                ViewBag.Cats = new SelectList(_catAppService.GetAll(), "Id", "Title");
                return View(dto);
            }
            dto.UserId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var res = _taskAppService.Add(dto);
            return RedirectToAction("Index", "User");
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Cats = new SelectList(_catAppService.GetAll(), "Id", "Title");
            var task = _taskAppService.GetById(id);
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
                ViewBag.Cats = new SelectList(_catAppService.GetAll(), "Id", "Title");
                return View();
            }
            _taskAppService.Update(dto,dto.Id);
            return RedirectToAction("Index", "User");
        }
        public IActionResult Delete(int id)
        {
            ViewBag.Cats = new SelectList(_catAppService.GetAll(), "Id", "Title");
            var task = _taskAppService.GetById(id);
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
                ViewBag.Cats = new SelectList(_catAppService.GetAll(), "Id", "Title");
                return View(dto);
            }
            _taskAppService.Delete(dto,dto.Id);
            return RedirectToAction("Index", "User");
        }

        //public IActionResult SearchAndSort(int userId,string searchTitle = "", string searchCategoryTitle = "", string orderByType = "Title")
        //{
        //    return View(_taskRepo.Filtering(userId, searchTitle, searchCategoryTitle, orderByType));
        //}
        //public IActionResult Index(int userId, string searchTitle = "", string searchCategory = "", string orderBy = "Title")
        //{
        //    var userId = int.Parse(User.FindFirst("UserId").Value); // یا هر claim دلخواه
        //    ViewBag.UserId = userId;
        //    return View();
        //}
        //public IActionResult Index()
        //{
        //    var userId = int.Parse(User.FindFirst("UserId").Value); // یا هر claim دلخواه
        //    ViewBag.UserId = userId;
        //    return View();
        //}
        //public IActionResult Index(string searchTitle, string searchCategoryTitle, string orderByType)
        //{
        //    var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        //    var tasks = _taskRepo.Filtering(userId, searchTitle, searchCategoryTitle, orderByType);

        //    // ارسال داده‌ها به View
        //    return View(tasks);  // در اینجا می‌تونی مدل یا ویو مناسب رو ارسال کنی
        //}
    }
}
