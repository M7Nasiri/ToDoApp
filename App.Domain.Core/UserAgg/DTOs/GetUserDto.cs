using App.Domain.Core.TaskAgg.DTOs;

namespace App.Domain.Core.UserAgg.DTOs
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? ImagePath { get; set; }

        public List<GetTaskDto>? Tasks { get; set; }
    }
}
