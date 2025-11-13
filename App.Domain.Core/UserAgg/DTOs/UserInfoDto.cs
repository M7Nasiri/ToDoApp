using App.Domain.Core.CategoryAgg.Entities;
using App.Domain.Core.TaskAgg.DTOs;

namespace App.Domain.Core.UserAgg.DTOs
{
    public class UserInfoDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string ImagePath { get; set; }
        public string CreatedAtFa { get; set; }
        public string DueDateFa { get; set; }
        public List<GetTaskDto>? Tasks { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
    }
}
