namespace App.EndPoints.MVC.ToDo.Models.ViewModels.User
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
