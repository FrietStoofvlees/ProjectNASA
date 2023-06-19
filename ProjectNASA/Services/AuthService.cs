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

            Task.Run(async () =>
            {
                await HasAuthenticationAsync();
            });
        }

        public async Task<bool> HasAuthenticationAsync()
        {
            if (AppHelpers.User is not null)
                return true;

            string hasAuth = await SecureStorage.Default.GetAsync("auth");
            if (!string.IsNullOrEmpty(hasAuth))
            {
                AppHelpers.User = userRepository.GetUser(hasAuth);
                return true;
            }
            return false;
        }

        public async Task<bool> SignInAsync(string username, string password)
        {
            try
            {
                //Password check
                User user = userRepository.GetUser(username);
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

        public bool SignOut(bool deleteProfile)
        {
            if (deleteProfile)
                userRepository.DeleteUser(AppHelpers.User);

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
                userRepository.SaveUser(user);
                await SecureStorage.Default.SetAsync("auth", user.Username);
                AppHelpers.User = user;

                return true;
            }
            catch (Exception)
            {
                await Shell.Current.DisplayAlert("Something went wrong!", $"The account could not be created, try again later please.", "OK");
                return false;
            }
        }
    }
}
