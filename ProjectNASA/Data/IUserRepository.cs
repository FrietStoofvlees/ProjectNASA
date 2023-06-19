namespace ProjectNASA.Data
{
    public interface IUserRepository
    {
        void DeleteUser(User user);
        User GetUser(string username);
        void SaveUser(User user);
    }
}
