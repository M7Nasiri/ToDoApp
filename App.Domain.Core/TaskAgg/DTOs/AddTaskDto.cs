using App.Domain.Core.TaskAgg.Enums;

namespace App.Domain.Core.TaskAgg.DTOs
{
    public class AddTaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public string? CreateAtFa { get; set; }
        public string? DueDateFa { get; set; }
        public int CategoryId { get; set; }
        //public List<Category> Categories { get; set; } = [];

        public int UserId { get; set; }

    }
}
