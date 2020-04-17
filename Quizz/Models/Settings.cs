using SQLite;

namespace Quizz.Models
{
    public class Settings
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool Sound { get; set; }
        public bool Notification { get; set; }

    }
}
