namespace ProjectNASA.Data
{
    public interface IUserRepository
    {
        Task DeleteUserAsync(User user);
        Task<User> GetUserAsync(string username);
        Task<bool> SaveUserAsync(User user);
    }
}
