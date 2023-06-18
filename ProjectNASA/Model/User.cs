using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ProjectNASA.Model
{
    [Table("users")]
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique, MaxLength(50)]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        [TextBlob("FavoriteApodsBlobbed")]
        public List<Apod> FavoriteApods { get; set; }
        public string FavoriteApodsBlobbed { get; set; }
    }
}
