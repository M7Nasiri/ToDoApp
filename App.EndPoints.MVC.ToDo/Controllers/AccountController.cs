using App.Domain.Core.UserAgg.Contracts.Services;
using App.Domain.Core.UserAgg.DTOs;
using App.EndPoints.MVC.ToDo.User.ViewModels;
using App.Infra.Data.FileStorageService.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace App.EndPoints.MVC.ToDo.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private readonly IFileService _fileService;
        public AccountController(IUserService userService, IFileService fileService)
        {
            _userService = userService;
            _fileService = fileService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }

            var result = _userService.Login(dto);

            if (result != null)
            {
                var claims = new List<Claim>() {
                new Claim(ClaimTypes.Name,dto.UserName),
                new Claim(ClaimTypes.NameIdentifier,result.Id.ToString()),
                };
                var identity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var properties = new AuthenticationProperties()
                {
                    IsPersistent = dto.RememberMe
                };
                HttpContext.SignInAsync(principal, properties);
                return RedirectToAction("Index", "User");
            }
            ViewBag.Error = "نام کاربری یا رمز عبور اشتباه است.";
            return View(dto);
        }


        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterUserViewModel model)
        {
            var file = model.ImageFile;

            string path = "";


            if (model.ImageFile != null)
            {
                using var stream = file.OpenReadStream();

                path = _fileService.Upload(stream, file.FileName, "Users");
            }
            var dto = new RegisterUserDto
            {
                FullName = model.FullName,
                ImagePath = path,
                Password = model.Password,
                UserName = model.UserName,
            };

            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            if (_userService.Register(dto))
            {
                return RedirectToAction("Login");
            }
            else
            {
                ViewBag.Error = "نام کاربری قبلا انتخاب شده است .";
                return View(model);
            }
        }
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }

    }
}
