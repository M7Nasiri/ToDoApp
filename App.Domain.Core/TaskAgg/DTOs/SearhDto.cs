using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Core.TaskAgg.DTOs
{
    public class SearchDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string SortBy { get; set; }
    }
}
