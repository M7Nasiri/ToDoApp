namespace App.Infra.Data.Db.InMemory
{
    public class OnlineUser
    {
        public int Id { get; set; }
        public string? Username { get; set; }
        public bool IsAdmin { get; set; }
    }
}
