using App.Domain.Core.BaseEntityAgg.Entities;
using App.Domain.Core.TaskAgg.DTOs;

namespace App.Domain.Core.TaskAgg.Contracts.Services
{
    public interface ITaskService
    {
        public Result<bool> Add(AddTaskDto dto);
        public Result<bool> Update(UpdateTaskDto dto, int id);
        public Result<bool> Delete(DeleteTaskDto dto, int id);
        public Result<List<GetTaskDto>> GetAll(int userId);
        public Result<GetTaskDto> GetById(int id);
    }
}
