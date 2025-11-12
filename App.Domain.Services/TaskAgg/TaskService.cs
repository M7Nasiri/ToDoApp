using App.Domain.Core.BaseEntityAgg.Entities;
using App.Domain.Core.TaskAgg.Contracts.Repositories;
using App.Domain.Core.TaskAgg.Contracts.Services;
using App.Domain.Core.TaskAgg.DTOs;
using App.Infra.Data.Tools;
namespace App.Domain.Services.TaskAgg
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;

        public TaskService(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public Result<bool> Add(AddTaskDto dto)
        {
            dto.CreatedAt = DateTime.Now;
            dto.CreateAtFa = dto.CreatedAt.ToPersianString("yyyy/MM/dd");

            dto.DueDateFa = dto.DueDate.ToPersianString("yyyy/MM/dd");


            var result = new Result<bool>();
            if (_taskRepository.Add(dto).IsSuccess)
            {
                result.Message = "تسک جدید با موفقیت اضافه شد .";
                result.IsSuccess = true;
            }
            else
            {
                result.Message = "تسک اضافه نشد .";
                result.IsSuccess = false;
            }
            return result;

        }

        public Result<bool> Delete(DeleteTaskDto dto, int id)
        {
            dto.DeletedAt = DateTime.Now;
            dto.IsDelete = true;
            var result = new Result<bool>();
            if (_taskRepository.Delete(dto, id).IsSuccess)
            {
                result.Message = "تسک جدید با موفقیت حذف شد .";
                result.IsSuccess = true;
            }
            else
            {
                result.Message = "تسک حذف نشد .";
                result.IsSuccess = false;
            }
            return result;
        }

        public Result<List<GetTaskDto>> GetAll(int userId)
        {
            var result = _taskRepository.GetAll(userId).Data;
            //result.ForEach(task => task.CreatedAt.ToPersianString("yyyy/MM/dd"));
            //result.ForEach(task => task.DueDate.ToPersianString("yyyy/MM/dd"));

            return new Result<List<GetTaskDto>>
            {
                Data = result,
                Message = "تسک های کاربر با موفقیت بازگردانده شدند ."
            };
        }

        public Result<GetTaskDto> GetById(int id)
        {
            return new Result<GetTaskDto>
            {
                Data = _taskRepository.GetById(id).Data,
            };
        }

        public Result<bool> Update(UpdateTaskDto dto, int id)
        {
            dto.UpdatedAt = DateTime.Now;
            dto.DueDateFa = dto.DueDate.ToPersianString("yyyy/MM/dd");

            var result = new Result<bool>();
            if (_taskRepository.Update(dto, id).IsSuccess)
            {
                result.Message = "تسک با موفقیت ویرایش شد .";
                result.IsSuccess = true;
            }
            else
            {
                result.Message = "تسک ویرایش نشد .";
                result.IsSuccess = false;
            }
            return result;
        }
    }
}
