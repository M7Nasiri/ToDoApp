namespace App.EndPoints.MVC.ToDo.User.ViewModels
{
    public class UpdateUserViewModel
    {
        public int Id { get; set; }
        public string? Password { get; set; }
        public string FullName { get; set; }
        public string? ImagePath { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
