using App.Domain.Core.TaskAgg.Enums;

namespace App.Domain.Core.TaskAgg.DTOs
{
    public class UpdateTaskDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public StatusEnum Status { get; set; }
        public bool IsDelete { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public string? DueDateFa { get; set; }


        public int CategoryId { get; set; }

    }
}
