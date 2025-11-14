using App.Domain.Core.Common.Contracts.Services;
using App.Domain.Core.TaskAgg.Contracts.AppServices;
using App.Domain.Core.TaskAgg.Contracts.Services;
using App.Domain.Core.UserAgg.Contracts.AppServices;
using App.Domain.Core.UserAgg.Contracts.Services;
using App.Domain.Core.UserAgg.DTOs;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;
using App.EndPoints.MVC.ToDo.Models.ViewModels.User;
using App.Domain.Core.TaskAgg.DTOs;
using App.Domain.Core.UserAgg.Entities;
using App.EndPoints.MVC.ToDo.Models.ViewModels.Task;

namespace App.EndPoints.MVC.ToDo.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly ITaskAppService _taskAppService;
        private readonly IUserAppService _userAppService;
        private readonly IFileService _fileService;

        public UserController(ITaskAppService taskAppService, IUserAppService userAppService, IFileService fileService)
        {
            _taskAppService = taskAppService;
            _userAppService = userAppService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            ViewBag.UserId = userId;
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var user = _userAppService.GetUserById(userId);
            var tasks = _taskAppService.GetAll(userId);
            var res  = new UserInfoDto
            {
                Tasks = tasks.Data,
                FullName = user.FullName,
                ImagePath = user.ImagePath,
                UserName = userName,
                Id = userId
            };
            var dto = new SearchDto { UserId = userId };
            var filtered = _taskAppService.Filtering(userId, dto);

            var taskSearchModel = new TaskSearchResultViewModel
            {
                Search = new SearchModel
                {
                    UserId = userId,
                    Title = "",
                    CategoryName = "",
                    SortBy = "Title"
                },
                Tasks = filtered.Data
            };
            ViewBag.TaskSearchModel = taskSearchModel;
            return View(res);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SearchAndSort(TaskSearchResultViewModel model)
        {
            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var dto = new SearchDto
            {
                UserId = userId,
                SortBy = model.Search.SortBy,
                CategoryName = model.Search.CategoryName,
                Title = model.Search.Title,
            };

            var result = _taskAppService.Filtering(userId, dto);
            var componentModel = new TaskSearchResultViewModel
            {
                Search = model.Search,
                Tasks = result.Data
            };
            // return Content("OK");
            return ViewComponent("TaskSearch", new { model = componentModel });
        }
        public IActionResult Edit(int id)
        {
            var user = _userAppService.GetUserById(id);
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
            var file = model.ImageFile;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string newPath = "";
            if (model.ImageFile != null)
            {
                _fileService.Delete(model.ImagePath);
                using var stream = file.OpenReadStream();
                newPath = _fileService.Upload(stream, file.FileName, "Users");
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
            _userAppService.Update(model.Id, dto);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var user = _userAppService.GetUserById(id);

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
            _userAppService.Delete(model.Id, model);
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }


        [HttpPost]
        public async Task<IActionResult> UpdateProfileImage(int id, UserProfileViewModel model)
        {
            var file = model.NewImage;
            string newPath = "";
            var user = _userAppService.GetUserById(id);

            if (ModelState.IsValid)
            {
                var dto = new UserProfileDto(); 
                if (model.NewImage != null && model.NewImage.Length > 0)
                {
                    _fileService.Delete(model.CurrentImagePath);
                    using var stream = file.OpenReadStream();
                    newPath = _fileService.Upload(stream, file.FileName, "Users");
                    model.CurrentImagePath = newPath;
                    dto.CurrentImagePath = newPath;
                }
                else
                {
                    dto.CurrentImagePath = user.ImagePath;
                }
                if (user.FullName != model.FullName)
                {
                    dto.FullName = model.FullName;
                }
                else
                {
                    dto.FullName = user.FullName;
                }
                
                    _userAppService.Update(id, dto);
                return RedirectToAction("Index");
            }

            return ViewComponent("UserProfile", new { userId = model.Id }); ;
        }

    }
}
