using App.Domain.Core.CategoryAgg.Entities;
using App.Domain.Core.TaskAgg.Enums;
using App.Domain.Core.UserAgg.Entities;
using App.Domain.Core.BaseEntityAgg.Entities;

namespace App.Domain.Core.TaskAgg.Entities
{
    public class MyTask : BaseEntity
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? CreatedAtFa { get; set; }
        public DateTime DueDate { get; set; }
        public string? DueDateFa { get; set; }
        public bool IsDelete { get; set; }


        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        public User? User { get; set; }
        public int UserId { get; set; }
    }
}
