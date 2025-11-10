namespace App.Domain.Core.UserAgg.DTOs
{
    public class RegisterUserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string? ImagePath { get; set; }


    }
}
