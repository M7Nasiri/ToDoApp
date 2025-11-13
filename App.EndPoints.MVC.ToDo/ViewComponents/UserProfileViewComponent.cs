using App.Domain.Core.UserAgg.Contracts.Services;
using App.EndPoints.MVC.ToDo.Models.ViewModels.User;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.ToDo.ViewComponents
{
    public class UserProfileViewComponent : ViewComponent
    {

        private readonly IUserService _userService;  
        public UserProfileViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke(int userId)
        {
            var user = _userService.GetUserById(userId);

            var model = new UserProfileViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                FullName = user.FullName,
                CurrentImagePath = user.ImagePath,
                TasksCount = user.Tasks.Count,
            };

            return View(model);
        }
    }
}
