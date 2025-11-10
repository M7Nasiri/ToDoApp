using App.Domain.Core.TaskAgg.Entities;

namespace App.Domain.Core.UserAgg.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? ImagePath { get; set; }
        public bool RememberMe { get; set; } = false;
        public List<MyTask>? Tasks { get; set; }
    }
}
