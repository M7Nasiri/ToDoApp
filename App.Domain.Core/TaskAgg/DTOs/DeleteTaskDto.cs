using App.Domain.Core.TaskAgg.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.TaskAgg.DTOs
{
    public class DeleteTaskDto
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public StatusEnum Status { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsDelete { get; set; }
        public DateTime DeletedAt { get; set; }
        public int CategoryId { get; set; }
        //public int UserId { get; set; }

    }
}
