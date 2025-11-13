namespace App.EndPoints.MVC.ToDo.Models.ViewModels.User
{
    public class GetUserViewModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public IFormFile? ImageFile { get; set; }

        public List<Task>? Tasks { get; set; }
    }
}
