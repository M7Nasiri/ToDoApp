using App.Domain.Core.TaskAgg.Enums;
using System.ComponentModel.DataAnnotations;

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

        //[RegularExpression(@"^\d{4}/((0\d)|(1[012]))/(([012]\d)|3[01])$", ErrorMessage = "تاریخ وارد شده معتبر نمی باشد .")]

        public DateTime DueDate { get; set; }
        public string? DueDateFa { get; set; }


        public int CategoryId { get; set; }

    }
}
