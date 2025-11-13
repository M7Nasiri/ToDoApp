using App.Domain.Core.TaskAgg.DTOs;

namespace App.EndPoints.MVC.ToDo.Models.ViewModels.Task
{
    public class TaskSearchResultViewModel
    {
        public SearchModel Search { get; set; } = new();
        public List<FilterTaskDto>? Tasks { get; set; } = new();
    }
}
