namespace ProjectNASA.Services
{
    public interface IAuthService
    {
        Task<bool> IsAuthenticated();
        Task<bool> SignIn(string username, string password);
        Task<bool> SignUp(string username, string password);
        bool SignOut();
    }
}
