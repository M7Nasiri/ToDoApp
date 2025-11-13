using App.Domain.Core.TaskAgg.Contracts.AppServices;
using App.Domain.Core.TaskAgg.Contracts.Repositories;
using App.Domain.Core.TaskAgg.DTOs;
using App.EndPoints.MVC.ToDo.Models.ViewModels.Task;
using Microsoft.AspNetCore.Mvc;

namespace App.EndPoints.MVC.ToDo.ViewComponents
{
    public class TaskSearchViewComponent : ViewComponent
    {
        private readonly ITaskAppService _taskAppService;

        public TaskSearchViewComponent(ITaskAppService taskAppService)
        {
            _taskAppService = taskAppService;
        }

        public IViewComponentResult Invoke(TaskSearchResultViewModel model)
        {
            // از مدل ورودی استفاده می‌کنیم که شامل اطلاعات جستجو و تسک‌هاست
            return View(model);
        }

    }
}
