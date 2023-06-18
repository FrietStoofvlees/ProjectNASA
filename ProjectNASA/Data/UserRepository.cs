using SQLite;

namespace ProjectNASA.Data
{
    public class UserRepository : IUserRepository
    {
        public string StatusMessage { get; set; }

        SQLiteAsyncConnection database;

        async Task Init()
        {
            if (database is not null)
            {
                return;
            }

            database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            CreateTableResult result = await database.CreateTableAsync<User>();
            StatusMessage = $"Table was {result}.";
        }

        public async Task DeleteUserAsync(User user)
        {
            int result = 0;

            try
            {
                await Init();

                result = await database.DeleteAsync(user);

                StatusMessage = $"{result} record(s) removed! (Name: {user.Username})";

            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to remove {result}. Error: {ex.Message}";
                throw;
            }
        }

        public async Task<User> GetUserAsync(string username)
        {
            try
            {
                await Init();

                User user = await database.GetAsync<User>(user => user.Username == username);

                return user;
            }
            catch (Exception ex)
            {
                StatusMessage = ex.Message;
                throw new UserNotFoundException(StatusMessage, username, ex);
            }
        }

        public async Task<bool> SaveUserAsync(User user)
        {
            int result = 0;

            try
            {
                await Init();

                if (user.Id != 0)
                    result = await database.UpdateAsync(user);
                else
                    result = await database.InsertAsync(user);

                StatusMessage = $"{result} record(s) saved (Name: {user.Username})";

                return result != 0;
            }
            catch (Exception ex)
            {
                StatusMessage = $"Failed to save {result} record(s). Error: {ex.Message}";
                throw;
            }
        }
    }
}
