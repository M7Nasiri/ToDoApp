using App.Domain.Core.BaseEntityAgg.Entities;
using App.Domain.Core.TaskAgg.Contracts.AppServices;
using App.Domain.Core.TaskAgg.Contracts.Services;
using App.Domain.Core.TaskAgg.DTOs;

namespace App.Domain.AppServices.TaskAgg
{
    public class TaskAppService : ITaskAppService
    {
        private readonly ITaskService _taskService;

        public TaskAppService(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public Result<bool> Add(AddTaskDto dto)
        {
            return _taskService.Add(dto);
        }

        public Result<bool> Delete(DeleteTaskDto dto, int id)
        {
            return _taskService.Delete(dto, id);
        }

        public Result<List<GetTaskDto>> GetAll(int userId)
        {
            return _taskService.GetAll(userId);
        }

        public Result<GetTaskDto> GetById(int id)
        {
            return _taskService.GetById(id);
        }

        public Result<bool> Update(UpdateTaskDto dto, int id)
        {
            return _taskService.Update(dto, id);
        }
    }
}
