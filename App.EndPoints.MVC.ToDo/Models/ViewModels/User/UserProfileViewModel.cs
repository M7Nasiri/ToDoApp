namespace App.EndPoints.MVC.ToDo.Models.ViewModels.User
{
    public class UserProfileViewModel
    {
        public int Id { get; set; }
        public string? CurrentImagePath { get; set; }
        public IFormFile? NewImage { get; set; }
        public string? FullName { get; set; }
        public string? UserName { get; set; }
        public int TasksCount { get; set; }
    }
}
