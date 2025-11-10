using App.Domain.Core.TaskAgg.Contracts.Services;
using App.Domain.Core.UserAgg.Contracts.Services;
using App.Domain.Core.UserAgg.DTOs;
using App.Domain.Core.UserAgg.Entities;
using App.EndPoints.MVC.ToDo.User.ViewModels;
using App.Infra.Data.FileStorageService.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.EndPoints.MVC.ToDo.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IUserService _userService;
        private readonly IFileService _fileService;

        public UserController(ITaskService taskService, IUserService userService, IFileService fileService)
        {
            _taskService = taskService;
            _userService = userService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var tasks = _taskService.GetAll(userId);
            var allUserInfo = new UserInfoDto
            {
                Tasks = tasks.Data,
                UserName = userName,
                Id= userId
            };
            return View(allUserInfo);
        }
        public IActionResult Edit(int id)
        {
            var user = _userService.GetUserById(id);
            if (user == null)
            {
                return View();
            }
            UpdateUserViewModel model = new UpdateUserViewModel()
            {
                FullName = user.FullName,
                Id = id,
                Password = user.Password,
                ImagePath = user.ImagePath
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Edit(UpdateUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string newPath = "";
            if (model.ImageFile != null)
            {
                _fileService.Delete(model.ImagePath);
                newPath = _fileService.Upload(model.ImageFile, "Users");
            }
            else
            {
                newPath = model.ImagePath;
            }
            var dto = new UpdateUserDto
            {
                Id = model.Id,
                FullName = model.FullName,
                Password = model.Password,
                ImagePath = newPath,
            };
            _userService.Update(model.Id, dto);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);

            var model = new DeleteUserDto
            {
                FullName = user.FullName,
                Id = id,
                UserName = user.UserName,
                ImagePath = user.ImagePath
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Delete(DeleteUserDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _userService.Delete(model.Id, model);
            return RedirectToAction("Login","Account");
        }
        
    }
}
