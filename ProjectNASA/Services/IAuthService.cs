namespace ProjectNASA.Services
{
    public interface IAuthService
    {
        Task<bool> HasAuthenticationAsync();
        Task<bool> SignInAsync(string username, string password);
        Task<bool> SignUpAsync(string username, string password);
        Task<bool> SignOut(bool deleteProfile);
    }
}
