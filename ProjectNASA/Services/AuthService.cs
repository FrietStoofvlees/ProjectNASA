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
                //await SecureStorage.GetAsync(user);
            }
            catch (Exception ex)
            {
                await Toast.Make(ex.Message).Show();
                return false;
            }

            if (Preferences.ContainsKey(nameof(AppHelpers.User)))
            {
                Preferences.Remove(nameof(AppHelpers.User));
            }

            string userDetails = JsonSerializer.Serialize(user);
            Preferences.Set(nameof(AppHelpers.User), userDetails);
            AppHelpers.User = user;

            return true;
        }

        public bool SignOut()
        {
            if (Preferences.ContainsKey(nameof(AppHelpers.User)))
            {
                Preferences.Remove(nameof(AppHelpers.User));
            }

            return true;
        }

        public bool SignOut(string username, string password)
        {
            if (Preferences.ContainsKey(nameof(AppHelpers.User)))
            {
                Preferences.Remove(nameof(AppHelpers.User));
            }

            return true;
        }
    }
}
