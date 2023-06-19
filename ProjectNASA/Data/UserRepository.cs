using SQLite;
using SQLiteNetExtensions.Extensions;

namespace ProjectNASA.Data
{
    public class UserRepository : IUserRepository
    {
        public string StatusMessage { get; set; }

        SQLiteConnection database;

        void Init()
        {
            if (database is not null)
                return;

            database = new SQLiteConnection(Constants.DatabasePath, Constants.Flags);
            CreateTableResult result = database.CreateTable<User>();
            StatusMessage = $"Table was {result}.";
        }

        public void DeleteUser(User user)
        {
            try
            {
                Init();

                int result = database.Delete(user);

                StatusMessage = $"{result} record(s) removed! (Name: {user.Username})";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to remove user: {user.Username}. Error: {ex.Message}";
                throw;
            }
        }

        public User GetUser(string username)
        {
            try
            {
                Init();
                User user = database.GetAllWithChildren<User>(user => user.Username == username).FirstOrDefault();

                return user;
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                throw new UserNotFoundException(StatusMessage, username, ex);
            }
        }

        public void SaveUser(User user)
        {
            try
            {
                Init();
                database.UpdateWithChildren(user);
                StatusMessage = $"User: {user.Username} saved.";
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to save user: {user.Username}. Error: {ex.Message}";
                throw;
            }
        }
    }
}
