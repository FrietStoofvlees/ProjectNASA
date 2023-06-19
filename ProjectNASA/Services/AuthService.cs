using ProjectNASA.Data;
using System.Text.Json;

namespace ProjectNASA.Services
{
    public class AuthService : IAuthService
    {
        readonly IUserRepository userRepository;

        public AuthService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> HasAuthenticationAsync()
        {
            if (AppHelpers.User is not null)
                return true;

            string hasAuth = await SecureStorage.Default.GetAsync("auth");
            if (!string.IsNullOrEmpty(hasAuth))
            {
                AppHelpers.User = await userRepository.GetUserAsync(hasAuth);
                return true;
            }
            return false;
        }

        public async Task<bool> SignInAsync(string username, string password)
        {
            User user = new()
            {
                Username = username,
                Password = password
            };

            try
            {
                user = await userRepository.GetUserAsync(user.Username);
                await SecureStorage.Default.SetAsync("auth", user.Username);
                AppHelpers.User = user;

                return true;
            }
            catch (UserNotFoundException ex)
            {
                await Shell.Current.DisplayAlert("Incorrect Username", $"The username {ex.UserName} doesn't belong to an account.", "Try Again");
                return false;
            }
        }

        public async Task<bool> SignOut(bool deleteProfile)
        {
            if (deleteProfile)
                await userRepository.DeleteUserAsync(AppHelpers.User);

            AppHelpers.User = null;
            return SecureStorage.Default.Remove("auth");
        }

        public async Task<bool> SignUpAsync(string username, string password)
        {
            User user = new()
            {
                Username = username,
                Password = password
            };

            try
            {
                bool result = await userRepository.SaveUserAsync(user);
                await SecureStorage.Default.SetAsync("auth", user.Username);
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
