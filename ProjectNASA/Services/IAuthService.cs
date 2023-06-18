namespace ProjectNASA.Services
{
    public interface IAuthService
    {
        Task<bool> SignIn(string username, string password);
        bool SignOut();
        bool SignOut(string username, string password);
    }
}
