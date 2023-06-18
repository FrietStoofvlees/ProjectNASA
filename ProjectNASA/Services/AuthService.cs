using CommunityToolkit.Maui.Alerts;
using ProjectNASA.Data;
using System.Text.Json;
using static SQLite.SQLite3;

namespace ProjectNASA.Services
{
    public class AuthService : IAuthService
    {
        IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> IsAuthenticated()
        {
            var hasAuth = await SecureStorage.GetAsync("hasAuth");
            return hasAuth != null;
        }

        public async Task<bool> SignIn(string username, string password)
        {
            User user = new()
            {
                Username = username,
                Password = password
            };

            try
            {
                user = await userRepository.GetUserAsync(user.Username);

                string userSerialized = JsonSerializer.Serialize(user);
                await SecureStorage.SetAsync(nameof(AppHelpers.User), userSerialized);
                AppHelpers.User = user;

                return true;
            }
            catch (UserNotFoundException ex)
            {
                await Shell.Current.DisplayAlert("Incorrect Username", $"The username {ex.UserName} doesn't belong to an account.", "Try Again");
                return false;
            }
        }

        public bool SignOut()
        {
            return SecureStorage.Remove(nameof(AppHelpers.User));
        }

        public async Task<bool> SignUp(string username, string password)
        {
            User user = new()
            {
                Username = username,
                Password = password
            };

            try
            {
                bool result = await userRepository.SaveUserAsync(user);

                string userSerialized = JsonSerializer.Serialize(user);
                await SecureStorage.SetAsync(nameof(AppHelpers.User), userSerialized);
                AppHelpers.User = user;

                return result;
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Something went wrong!", $"The account could not be created, try again later please.", "OK");
                return false;
            }
        }
    }
}
