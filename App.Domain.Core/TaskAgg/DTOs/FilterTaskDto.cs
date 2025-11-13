using App.Domain.Core.TaskAgg.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.TaskAgg.DTOs
{
    public class FilterTaskDto
    {
        public int Id { get; set; }
        public string searchTitle { get; set; }
        public string searchCategory { get; set; }
        public string? Title { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public string? DueDateFa { get; set; }
        public DateTime CreateAt { get; set; }
        public string? CreatedAtFa { get; set; }
        //public int CategoryId { get; set; }
        public string? CategoryName { get; set; }
        //public List<Category> Categories { get; set; } = [];

        public int UserId { get; set; }
    }
}
