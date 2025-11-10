namespace App.EndPoints.MVC.ToDo.User.ViewModels
{
    public class RegisterUserViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
