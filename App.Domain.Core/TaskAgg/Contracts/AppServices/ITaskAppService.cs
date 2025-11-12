using App.Domain.Core.BaseEntityAgg.Entities;
using App.Domain.Core.TaskAgg.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.TaskAgg.Contracts.AppServices
{
    public interface ITaskAppService
    {
        public Result<bool> Add(AddTaskDto dto);
        public Result<bool> Update(UpdateTaskDto dto, int id);
        public Result<bool> Delete(DeleteTaskDto dto, int id);
        public Result<List<GetTaskDto>> GetAll(int userId);
        public Result<GetTaskDto> GetById(int id);
    }
}
