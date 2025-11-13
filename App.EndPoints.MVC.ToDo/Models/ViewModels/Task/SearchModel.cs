namespace App.EndPoints.MVC.ToDo.Models.ViewModels.Task
{
    public class SearchModel
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string CategoryName { get; set; }
        public string SortBy { get; set; }
    }
}
