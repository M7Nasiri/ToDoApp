using App.Domain.Core.Common.Contracts.Services;
using App.Domain.Core.TaskAgg.Contracts.AppServices;
using App.Domain.Core.TaskAgg.Contracts.Services;
using App.Domain.Core.UserAgg.Contracts.AppServices;
using App.Domain.Core.UserAgg.Contracts.Services;
using App.Domain.Core.UserAgg.DTOs;
using App.EndPoints.MVC.ToDo.User.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Security.Claims;

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
            var userName = User.FindFirstValue(ClaimTypes.Name);
            var tasks = _taskAppService.GetAll(userId);
            var allUserInfo = new UserInfoDto
            {
                Tasks = tasks.Data,
                UserName = userName,
                Id = userId
            };
            return View(allUserInfo);
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

    }
}
