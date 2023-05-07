namespace ProjectNASA.Services
{
    public interface ILoginService
    {
        bool Login(string username, string email, string password);
        bool Logout();
        bool Logout(string username, string password);
    }
}
