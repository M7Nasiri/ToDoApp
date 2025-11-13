using App.Domain.Core.BaseEntityAgg.Entities;
using App.Domain.Core.TaskAgg.Contracts.Repositories;
using App.Domain.Core.TaskAgg.DTOs;
using App.Domain.Core.TaskAgg.Entities;
using App.Infra.Data.Db.SqlServer.Ef.DbCtx;
using Microsoft.EntityFrameworkCore;

namespace App.Infra.Data.Repo.Ef.TaskAgg
{
    public class TaskRepository : ITaskRepository
    {
        private readonly AppDbContext _context;
        public TaskRepository(AppDbContext context)
        {
            _context = context;
        }
        public Result<bool> Add(AddTaskDto dto)
        {
            var task = new MyTask
            {
                CategoryId = dto.CategoryId,
                CreatedAt = dto.CreatedAt,
                CreatedBy = dto.UserId,
                UserId = dto.UserId,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsDelete = false,
                Status = dto.Status,
                Title = dto.Title,
                CreatedAtFa = dto.CreateAtFa,
                DueDateFa = dto.DueDateFa,
            };
            _context.Add(task);
            var res = _context.SaveChanges() > 0;
            return new Result<bool>
            {
                IsSuccess = res,
            };
        }

        public Result<bool> Delete(DeleteTaskDto dto, int id)
        {
            //var res = _context.Tasks.Where(t => t.Id == id).ExecuteDelete() > 0;
            //var task = GetById(id);
            //if(task.Data == null)
            //{
            //    return new Result<bool>
            //    {
            //        IsSuccess = false,
            //    };
            //}
            //task.Data.IsDelete = true;
            //task.Data.DeletedAt = dto.DeletedAt;
            var res = _context.Tasks.Where(t => t.Id == id).ExecuteUpdate(setters => setters
            .SetProperty(t => t.IsDelete, true)
            .SetProperty(t => t.DeletedAt, DateTime.Now)) > 0;

            return new Result<bool>
            {
                IsSuccess = res,
            };
        }

        public Result<List<GetTaskDto>> GetAll(int userId)
        {
            var res = _context.Tasks.Include(t=>t.Category).AsNoTracking().Where(t => t.UserId == userId).Select(t => new GetTaskDto
            {
                CategoryId = t.CategoryId,
                Description = t.Description,
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate,
                Id = t.Id,
                Status = t.Status,
                Title = t.Title,
                Category = t.Category,
                CreatedAtFa = t.CreatedAtFa,
                DueDateFa = t.DueDateFa
            }).ToList();
            return new Result<List<GetTaskDto>> { Data = res };
        }

        public Result<GetTaskDto> GetById(int id)
        {
            var res = _context.Tasks.AsNoTracking().Where(t => t.Id == id).Select(t=>new GetTaskDto
            {
                CategoryId = t.CategoryId,
                Description = t.Description,
                CreatedAt = t.CreatedAt,
                DueDate = t.DueDate,
                Id = t.Id,
                Status = t.Status,
                Title = t.Title,
            }).FirstOrDefault();
            return new Result<GetTaskDto> { Data = res };
        }

        public Result<bool> Update(UpdateTaskDto dto, int id)
        {
            var res = _context.Tasks.Where(t => t.Id == id).ExecuteUpdate(setters => setters.SetProperty(t => t.Title, dto.Title)
            .SetProperty(t => t.IsDelete, dto.IsDelete)
            .SetProperty(t => t.Status, dto.Status)
            .SetProperty(t => t.DueDate, dto.DueDate)
            .SetProperty(t => t.DueDateFa, dto.DueDateFa)
            .SetProperty(t => t.UpdatedAt, dto.UpdatedAt)
            .SetProperty(t => t.CategoryId, dto.CategoryId)
            .SetProperty(t => t.Description, dto.Description)
            ) > 0;
            return new Result<bool>
            {
                IsSuccess = res
            };
        }

        public Result<List<FilterTaskDto>> Filtering(int userId,SearchDto dto)
        {
            IQueryable<MyTask> result = _context.Tasks.Where(t=>t.UserId == userId).Include(t=>t.Category);
            if (!string.IsNullOrEmpty(dto.Title))
            {
                result = result.Where(t => t.Title.Contains(dto.Title));
            }
            if (!string.IsNullOrEmpty(dto.CategoryName))
            {
                result = result.Where(t=>t.Category.Title.Contains(dto.CategoryName));
            }
            switch (dto.SortBy)
            {
                case "Title":
                    result = result.OrderByDescending(t => t.Title);
                    break;
                case "DueDate":
                    result = result.OrderByDescending(t => t.DueDate);
                    break;
                
                case "Status":
                    result = result.OrderByDescending(t => t.Status);
                    break;
            }
            var res = result.Select(t => new FilterTaskDto
            {
                Status = t.Status,
                DueDate = t.DueDate,
                CategoryName = t.Category.Title,
                Title = t.Title,
                CreatedAt = t.CreatedAt,
                CreatedAtFa = t.CreatedAtFa,
                DueDateFa = t.DueDateFa,
                Id = t.Id,
                UserId = t.UserId,
            }).ToList();
            return new Result<List<FilterTaskDto>> 
            {
                IsSuccess = true,
                Data = res 
            };
        }
    }
}
